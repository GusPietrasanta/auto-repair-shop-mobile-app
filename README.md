# Auto Repair Shop Mobile App

## Description

The mobile app makes use of the [auto-repair-shop-api](https://github.com/GusPietrasanta/auto-repair-shop-api) to provide another platform to mechanics to perform inspections/complete reports and send messages to the manager.

## Features 

### Login Page 
Users can log in using their credentials (username and password) to gain access to the app's functionalities. The app will store the token that the API provided into the session, and use it for all the operations that require authorisation.

### Dashboard 
Upon successful login, the dashboard displays a welcome message with the user's username and the number of jobs assigned for the day. Users can navigate to the "JobsPage" to view detailed information about the assigned jobs. There is also a section to send messages to the manager.

### Jobs Page 
The "JobsPage" lists all the appointments assigned to the user, showing essential details about each job. Mechanics can click the "Start Job" button to proceed to the "ReportPage" for a specific appointment.

### Report Page 
The "ReportPage" allows mechanics to create detailed reports for each job. The page presents a range of inspection options for various vehicle components, such as air conditioning, lights, tires, fluids, brakes, and more. Mechanics can add comments and notes for each component. After completing the inspection, they can save the report, which is then sent to the server via the API to be stored in the database, where the manager can view it through the [auto-repair-shop-management web app](https://github.com/GusPietrasanta/auto-repair-shop-management)

## Technology Stack

- C#
- Xamarin.Forms
- APIs
- JWT (Json Web Tokens)

## Screenshots

Wrong log in details will return a 401 Unauthorized from the API, and the app will let the user know that the login details were incorrect.

![](https://lh3.googleusercontent.com/pw/AIL4fc-23ZQiJEMy9GzQI2zM4hmCzpIitoS16gsK1C70h5eiRSX_vTIyjbeDLO1dgskGoXKukyI6j6Ff8Vzy0f0lFYKwlhsYwrY-lwEZU3KlwK4CEN9bwoyt310vGtmBczjTMWDlpHLTexxaE3CFrX04yM9O=w441-h931-s-no)

Welcome screen for different users.

![](https://lh3.googleusercontent.com/pw/AIL4fc8RDNYqcv_I4LpNnS8Y8zDDsI_yqnwAdybpzBm2MRT4kP9tVMlcz-CBC6TWOwiwkTkDxxqbP0qh0MLZqMY13t_HLqJtJIgdNN7dtuTJLVgDG00ym5Hsn4l-iqV9OSPKfoqDvbX6AaiiP4vxscRBeyCA=w1920-h403-s-no)

Jobs Page, where the mechanics can see details about every appointment for the day.

![](https://lh3.googleusercontent.com/pw/AIL4fc_iCEHERXdfAFt9Lif04R8TW5JPjnMZviXq-S8tBplX2EVBZfSmZ0YMOai6onSigGNP6fXmOz3wcu9JDSPyH552mf6m1mLpUb2NZFIbfOT7YfaNcx1FplgEscoStzcRLsmUHbGqggR2WkH-Phg71nTV=w433-h854-s-no)

Sending a message through the app (on the right the manager's dashboard show the new message popping up).

![](https://lh3.googleusercontent.com/pw/AIL4fc8GnmynhmjtMmym1zyGyzKDtHoLZ56xDgclwQBsOYQwHHe18w6zbdMlVzZFpKM3GNStPymEu12bflvZXEfHENJPPexZM1RhC0Bq3--VzUKfjSpixx-3WhP5Y-cpFFoIn7PrebwDL5IOUF9duf1qm_F2=w1423-h874-s-no)

Saving a report sends the information to the server through the API, and will automatically appear as complete on manager's dashboard.

![](https://lh3.googleusercontent.com/pw/AIL4fc-MAxgBANnkV8X6VdqHD6Osx8FQApFlitJCUTPwcdafatrkljy65TbM29fRUL6HtMJ9BfAjnBeyyAV1ahnDQWuewC0yQjfmupBJ4piQFg83D-uQfR8uQypPV9Ek6-_YQL5Me8KbHoPGd_Y2z-zMA-Bs=w700-h465-s-no)


## App Working Together with the API deployed to AWS and the Web App to Azure

![](https://lh3.googleusercontent.com/pw/AIL4fc_qDTlL_SmHqop5KzHfSqK3M3PAYdVpI5qzwrj_GaEIEr6A2djIBjL7FzTFNiOFwbCrHbj3Kr08bEr5eE3vIWYWWZtduXhfW1ZSguOtur3rK5KOUiheIhx5DdvYexeiZjvfAag_NJgP9yWe74XI9YnQ=w1824-h912-s-no)


## Licence
[MIT Licence] (http://opensource.org/licenses/MIT)