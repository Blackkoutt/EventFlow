# <img width="369" height="66" alt="logo" src="https://github.com/user-attachments/assets/af60ea02-3591-4189-a66a-c09c283b7c8d" />
# 📑 Table Of Content

- [General info](#general-info)
- [Technologies](#technologies)
- [External Integrations](#integrations)
- [Getting Started](#getting-started)

<h1 id="general-info"></h1>
  
# ℹ️ General info
EventFlow is a web application that streamlines the management of cultural events covering everything from event planning to ticket distribution. It also allows users to purchase tickets, rent halls, and buy event passes.

<h1 id="technologies"></h1>

# 🧰 Technologies  
Project is created with:

<p align="center">
  <a href="https://learn.microsoft.com/en-us/dotnet/" target="_blank">
    <img width="120" src="https://github.com/user-attachments/assets/e788c988-8069-4c32-9a78-a778dd7c941c" alt=".NET" />
  </a>
    ㅤㅤㅤㅤㅤㅤㅤ
  <a href="https://learn.microsoft.com/en-us/sql" target="_blank">
    <img width="120" alt="image" src="https://github.com/user-attachments/assets/0e6806d4-aac2-4cba-b8d6-b80c40c2c2cb" />
  </a>
    ㅤㅤㅤㅤㅤㅤㅤ
  <a href="https://react.dev/learn" target="_blank">
    <img width="120" alt="image" src="https://github.com/user-attachments/assets/0f3389c3-a67b-44d4-a5f3-ab8d89aaad34" />
  </a>
</p>

<p align="center">
  <a href="https://vite.dev/guide/" target="_blank">
    <img width="120" alt="image" src="https://github.com/user-attachments/assets/50f7d3df-2ca9-4786-b722-6e841a479986" />
  </a>
    ㅤㅤㅤㅤㅤㅤㅤ
  <a href="https://tailwindcss.com/docs" target="_blank">
    <img width="120" alt="image" src="https://github.com/user-attachments/assets/f5fb9ba8-95d2-49de-bade-2b48eedf6515" />
  </a>
    ㅤㅤㅤㅤㅤㅤㅤ
  <a href="https://www.typescriptlang.org/docs/" target="_blank">
    <img width="120" alt="image" src="https://github.com/user-attachments/assets/346706f7-7f9b-4dd6-8d3d-535fb7d38c26" />
  </a>
</p>

<h1 id="integrations"></h1>

# 🧩 External Integrations
Project uses the following external integrations:
- **PayU API** <img width="90" alt="image" src="https://github.com/user-attachments/assets/758a2cc3-b205-408c-89b4-2adb7086beb6" /> 
- **Azure Blob Storage** <img width="70" alt="image" src="https://github.com/user-attachments/assets/1ad239b4-1097-4f97-9ffd-86caaa0f29b8" />
- **Google and Facebook OAuth** <img width="60" alt="image" src="https://github.com/user-attachments/assets/079a360e-ac70-420c-a02b-42ffaac6d268" /> <img width="60" alt="image" src="https://github.com/user-attachments/assets/716d2acf-f7a8-44e9-9eb0-b6be42775ad2" />

<h1 id="getting-started"></h1>

# 🚀 Getting Started

### 📥 Clone the Repository
```bash
git clone https://github.com/Blackkoutt/TeslaGo.git
```

### 🛠️ Configure the Database Connection
Open the following files and update the connection string with your own MSSQL database:

- **EventFlowAPI/EventFlowAPI/appsettings.json**  
  Paste your connection string in the key:

  ```json
  "ConnectionStrings": {
    "MSSQLEventFlowDB": "YourConnectionStringHere"
  }
  ```

- **EventFlowAPI/EventFlowAPI.DB/Context/APIContextFactory.cs**  
  Inside the method `optionsBuilder.UseSqlServer();`, replace the connection string accordingly.

---

### ▶️ Run the Project Using the `.bat` Script
Simply run the provided batch script to set everything up automatically:

```bash
run.bat
```

This script will:
- 📦 Install all necessary dependencies  
- 🧱 Apply database migrations  
- 🌐 Launch the web servers automatically  

Just **double-click `run.bat`**, and you’re good to go 🚀!

# 
<p align="right">
  <h5 align="right">© 2025 Blackkoutt •</b> <img width="100" height="66" alt="logo" src="https://github.com/user-attachments/assets/af60ea02-3591-4189-a66a-c09c283b7c8d" />
</p>


