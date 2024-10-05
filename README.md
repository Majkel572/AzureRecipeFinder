

# Recipe Finder
![our logo](https://github.com/Majkel572/AzureRecipeFinder/blob/mobile-app/app/src/debug/res/drawable/logo_color.png)
#### Contributors

- [Michał Ziober](https://github.com/micha5555)
- [Maja Nagarnowicz](https://github.com/nebraszka)
- [Jakub Maciejewski](https://github.com/PiorunPL)
- [Jakub Urbański](https://github.com/urbanski220)
- [Mikołaj Guryn](https://github.com/Majki572)
- [Jakub Goliszewski](https://github.com/jgoliszewski)

We are third-year students of Applied Computer Science at the Faculty of Electrical Engineering at Warsaw University of Technology.

Our project was developed as part of the *Introduction to Artificial Intelligence and Microsoft Azure-based Applications and Solutions* course.

### Purpose of the project

The aim of the project was to create an app that would provide a recipe for a particular dish based on a photo uploaded by its user.

### Functionality description

Based on the photo of the dish provided by the user, the application recognises the dish presented in the image, returns the ingredients needed to make this dish and the recipe.
The application requires you to log in first.

### Application demo

[Demo of our application](https://www.youtube.com/watch?v=KMOndmBJCx8)

### Map of technologies

![diagram](https://github.com/Majkel572/AzureRecipeFinder/blob/main/diagram.drawio_page-0001.jpg)
##### Used Azure services:
- Azure Machine Learning - developing a food recognition model
- Azure Container Registry - deploy Azure machine learning models as a secure endpoint
- Azure Storage Account - storage of files 
- Active Directory - logging in to the application with a Microsoft account
- Web App Service - enables the creation of an endpoint for communication between the ml blob and the user
- Microsoft Cost Management - allows you to keep track of expenses related to the use of Azure services, in particular Azure Machine Learning
- Azure Key Vault - contains connection string to blob storage
- Azure Advisor 
- Azure Monitor

##### Other technologies used:
- Python - it was used in a notebook in the Azure Machine Learning service to create and train a model, and then to create an endpoint with the finished model. It was also used to process the dataset and insert recipes into the blob
- ASP.NET Core 6.0 - developing the API
- Github - code hosting platform for version control and collaboration

### Usage - web application
To use our web application you need to navigate to the [link](https://recipefinder7.azurewebsites.net/) and then press the **Sign In** button at the top right of the website. Once logged in, click on the **Upload** section located in the top right corner of the website. Once you have selected an image from your computer storage, press the **Upload Image** button. The download of the file *recipe.txt* will automatically start. 
- The first line of this file contains *the name* of the recognised dish,
- the next paragraph contains *the ingredients* needed to make the dish,
- the last paragraph contains *the instructions* for making the dish.
