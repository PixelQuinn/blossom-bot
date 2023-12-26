# Blossom Bot

Blossom Bot is a Discord bot built using DSharpPlus and CommandsNext in C#. It provides various fun and utility commands to enhance your Discord server experience.

## Table of Contents
- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Configuration](#configuration)
- [Command Examples](#command-examples)
- [Documentation](#documentation)
- [Contributing](#contributing)
- [License](#license)

## Features
- Greeting users
- Basic arithmetic commands
- Poll creation
- Reminder setting
- Coin flipping and more!

## Getting Started

### Prerequisites
- .NET SDK 
- Discord Bot Token

### Installation
1. Clone the repository: `git clone https://github.com/your-username/blossom-bot.git`
2. Navigate to the project directory: `cd blossom-bot`
3. Build the project: `dotnet build`
4. Run the bot: `dotnet run`

### Configuration
1. Create a `config.json` file in the root directory.
2. Add your Discord Bot Token to the `token` field.
json
{
  "token": "YOUR_DISCORD_BOT_TOKEN",
  "prefix": "!"
}

### Command Examples

- Greet users: !greet

- Perform arithmetic operations: !add 5 3 (adds 5+3 and displays result)

- Create a poll: !poll "Favorite Color?" "Red" "Blue" "Green"

- Set a reminder: !remind 10:00:00 "Meeting in 10 hours!"

- Flip a coin: !flipcoin

- Roll Dice: !roll 2 10, this command will roll 2d10. 

- Random Number: !rng 1 10, generates a random number between 1-10. 

### Contributing
Contributions are welcome! Fork the repository, make changes, and submit a pull request.


Replace placeholders like `YOUR_DISCORD_BOT_TOKEN`, `your-username`, with your actual values.

