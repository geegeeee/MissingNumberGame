# Missing Number Game 🎯

A simple console application written in C# that helps users find the missing number from a sequence of unique integers ranging from 0 to _n_, where one number is missing.

## 📌 Features

- Takes user input of numbers (e.g., `3,0,1`) via console
- Validates the input:
  - No duplicates allowed
  - All numbers must be in range [0, n]
- Calculates the missing number using arithmetic sum formula
- Error handling and retry mechanism for invalid inputs
- Clean, modular code using interfaces and separation of concerns

## 🛠️ Tech Stack

- Language: C#
- Runtime: .NET 6+ (or any version compatible with C# 9.0+)
- IDE: Visual Studio Code or Visual Studio

## 🚀 How to Run Locally

### ✅ Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine
- A terminal or command line interface

### 📦 Steps

```bash
# 1. Clone this repository
    git clone https://github.com/geegeeee/MissingNumberGame.git
    cd MissingNumberGame

# 2. Build the project
    dotnet build

# 3. Run the application
    dotnet run

##💡 How to Use

After you run the program:
1. You'll be prompted to enter a series of comma-separated numbers (e.g., 3,0,1).
2. The app will find and display the missing number in the sequence.
3. If the input format is wrong or has issues (like duplicates or out-of-range values), it will let you retry.

##📁 Project Structure
MissingNumberGame/
├── Program.cs                # Main entry point and game logic
├── README.md                 # Project documentation
└── (bin/ obj/)               # Build artifacts

##🤝 Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.