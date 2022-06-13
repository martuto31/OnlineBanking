# Description
Online Banking website written on ASP.NET using EFcore with mssql database.

# Features
• Login/Register form and Session which holds the currently logged in user for 5 minutes<br />
• Add debit card form<br />
• Depositing from 1 debit card to another<br />
• Currency converter which converts the deposited amount of funds from the debit card which sends the money to the currency of the debit card which receives the money<br />
• Information tab which holds all of the information about the user<br />
• Information about all of the user's transactions<br />

# Additional information
Every user has a bank account which can have many debit cards. The funds of the bank account is the sum of the funds in all of his debit cards.
Every deposit creates 2 new transactions in the database, 1 for each debit card.

# Workflow
![ezgif com-gif-maker (1)](https://user-images.githubusercontent.com/47303509/173378954-275babe5-418a-4a4b-9204-7a8d17a1fbeb.gif)
