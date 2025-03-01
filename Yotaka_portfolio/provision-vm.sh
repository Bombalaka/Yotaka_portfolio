#!/bin/bash

# ========================
# variables
# ========================
RESOURCE_GROUP_NAME=dotnetdemo
VM_NAME=dotnetdemo-vm
PORT=5000
LOCATION = northeurope
ADMIN_USER = azureuser
RUNTIME = "9.0"

# local paths
LOCAL_APP_DIR=$(cygpath -u "C:\Users\yotak\Yotaka_portfolio\Yotaka_portfolio")
LOCAL_PUBLISH_DIR=$(cygpath -u "C:\Users\yotak\Yotaka_portfolio\Yotaka_portfolio\bin\Release\net9.0\publish")

# ========================
# step 1: Create a resource group.
# ========================
echo "Creating Azure resource group: $RESOURCE_GROUP in $LOCATION..."
az group create --name $RESOURCE_GROUP_NAME --location $LOCATION

# ========================
# step 2: Create a virtual machine.
# ========================
echo " Creating VM: $VM_NAME..."
az vm create \
  --resource-group $RESOURCE_GROUP_NAME \
  --name $VM_NAME \
  --image Ubuntu2404 \
  --size Standard_B1s \
  --admin-username $ADMIN_USER \
  --generate-ssh-keys\
  --output none || exit 1 # exit if error gor recommend from grok ai. make sure which are you now.  
  
# ========================
# step 3: Open port 5000.
# ========================
az vm open-port \
  --resource-group $RESOURCE_GROUP_NAME \
  --name $VM_NAME \
  --port $PORT\
  --output none || exit 1

# ========================
# step 4: Get IP public address of the VM.
# ========================
echo "Getting public IP address of the VM..."
PUBLIC_IP=$(az vm show \
  --resource-group $RESOURCE_GROUP_NAME \
  --name $VM_NAME \
  --show-details \
  --query [publicIps] \
  --output tsv)\
    || exit 1

# ========================
# step 5: Publish the .net mvc app .
# ========================
echo "Publishing the .NET MVC app..."
cd "$LOCAL_APP_DIR" || {echo "Directory not found: $LOCAL_APP_DIR"; exit 1;}
dotnet publish $LOCAL_APP_DIR -o $LOCAL_PUBLISH_DIR || {echo "Failed to publish the app."; exit 1;}
echo "build successful"

# ========================
# step 5: Deploy app to the vm .
# ========================
echo "Deploying the app to the VM..."
scp -r -o StrictHostKeyChecking=no -o UserKnownHostsFile=/dev/null "$LOCAL_PUBLISH_DIR" $ADMIN_USER@$PUBLIC_IP:/opt/Yotaka_portfolio/ || {echo "Failed to deploy the app."; exit 1;}
echo "Deployment successful"

# ========================
# step 6: Deploy app to the vm and configure enviroment.
# ========================
echo "Deploying the app to the VM..."
ssh -o StrictHostKeyChecking=no $ADMIN_USER@$PUBLIC_IP << EOF
    echo "Installing .NET runtime..."
    sudo apt-get update
    wget https://packages.microsoft.com/config/ubuntu/24.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
    sudo dpkg -i packages-microsoft-prod.deb
    rm packages-microsoft-prod.deb

    # update package 
    sudo apt-get update
    sudo apt-get install -y dotnet-sdk-9.0
    sudo apt-get install -y aspnetcore-runtime-9.0

    # create a directory for the app
    sudo mkdir -p /opt/Yotaka_portfolio

    # copy the app to the app directory
    sudo cp -r /opt/Yotaka_portfolio/* /opt/Yotaka_portfolio/

    echo "Create service file for the application..."
    sudo bash -c "cat > /etc/systemd/system/Yotaka_portfolio.service << 'INNER_EOF'
    [Unit]
    Description=Yotaka_portfolio
    [Service]
    WorkingDirectory=/opt/Yotaka_portfolio
    ExecStart=/usr/bin/dotnet /opt/Yotaka_portfolio/Yotaka_portfolio.dll
    Group=www-data
    User=www-data
    Environment=DOTNET_ENVIROMENT=Production
    Environment=ASPNETCORE_URLS=http://0.0.0.0:5000
    [Install]
    WantedBy=multi-user.target
    INNER_EOF"
    
    # Enable and start the service
    sudo systemctl enable Yotaka_portfolio.service
    sudo systemctl start Yotaka_portfolio.service

    echo "Starting the app..."
EOF
# ========================
# step 7: Final confirmation .
# ========================
echo "The app is running at http://$PUBLIC_IP:5000"

  
