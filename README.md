# 🌍 .NET Web App Deployment on Azure

## 🚀 Project Overview
This project is a **.NET web application** deployed on an **Azure Virtual Machine (Ubuntu 24.04)** using **Nginx as a reverse proxy**. The app is configured as a systemd service and runs on **port 5000**, with external traffic being routed through **port 80** via Nginx.

## ⚙️ **Prerequisites**
Before setting up the project, ensure you have:
- An **Azure subscription**
- **Azure CLI** installed (`az --version`)
- **.NET SDK 9.0** installed (`dotnet --version`)
- **SSH access** to the Azure VM

## 🚀 **How to Deploy the App**
### **1️⃣ Create the Azure VM**
Run the provisioning script to create and configure the VM:
```bash
provision-1.sh
```
After running the script, check the public IP of your VM:
```bash
az vm show --resource-group nameofresource_group --name nameofvm --show-details --query publicIps -o tsv
```
Then, open your browser and visit:
```bash
http://<your-vm-ip>
```
Now .NET app should be running! 🎉