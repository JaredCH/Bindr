# 📎 Bindr

**Bindr** is a Windows Forms utility app built to automate and streamline document workflows related to JDE Riders, BOMs, NestPlans, and more — tailored for use in an industrial or manufacturing environment.

---

## 🚀 Key Features

### 🧠 Smart PDF Extraction + Merging
- Load in a PDF (e.g., a JDE Rider).
- Automatically extract specific text from each page.
- Match and merge support detail PDFs with the Rider PDF using the extracted data.

---

### 📄 Process BOM
- Open a BOM CSV file.
- Automatically:
  - Replace blank fields with `0`.
  - Strip out odd characters like `%`.
  - Replace `#` symbols with `0`.
- Cleaned data is copied to the clipboard, ready to paste into the PS tool.

---

### 📥 Load SO / Load BOM
- Reads `.xlsv` files in the background (non-blocking).
- Extracts relevant SO or BOM data and places it on the clipboard.
- Designed for quick transfer into JDE with no manual cleanup.

---

### 🗺️ PDF Coordinate Mapping (NEW!)
- Define up to **4 mappable coordinates** on a 'smart' PDF (text-based, not scanned images).
- Bindr processes every page using the mapped areas to extract relevant data across the document.

---

### 🧾 NestPlan Data Viewer (NEW!)
- Parses and displays metadata from NestPlan files (plasma metal cutter machine instructions).
- Extracts part numbers and key attributes.
- Includes a **right-click preview** option to show NestPlan PDF or related support detail (initial functionality implemented).
- Future support planned: live data access via Epicor SQL integration.

---

## ⚙️ Under the Hood

- Built in **C# WinForms**.
- Uses **Pdfium** for PDF rendering and text extraction.
- Optimized for clipboard workflows, so you can move clean, parsed data directly into downstream tools like JDE and the PS tool with a single paste.

---

## 🛠️ Planned Features

- 🔗 SQL integration with Epicor for live support and NestPlan lookup.
- 🖼️ Thumbnail previews of matched support files or NestPlan documents.
- 📋 Batch PDF merge automation.

---

## 📂 Installation

Clone this repo and open the project in Visual Studio:

```bash
git clone https://github.com/JaredCH/Bindr.git
