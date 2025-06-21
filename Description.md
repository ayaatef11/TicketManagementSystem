


1- Architecture:
used 3-n archcitecture

 DAL:
 handles the data from database
it consists of:
Data folder:
handle the communication between the prgram and the database it contatins configurations for the models like description , full name to be required and limit the length of  string attributes to be 30 so a hacker can't put long string that harms the database

stores folder:
contains the ticketContext for database interaction
DTOS and also validatiosns for DTOS that are passed later to the BLL layer

enums folder:
for priority and status and they are configured to be passed to the database as strings
migrations folder:
generated while making migrations from the program to database
models folder:
the core entities and they inherit from the base entity which contains the id as it would be used later in the unit of work to be passed as T 

validations folder:
for the models 

parameters folder:
to handle the parameters given for filteration as in the future they may e bigger in count so we handle them in the class

------------------------------------------------
BLL layer 
it is the bussiness layer where we handle services and repositories

I used for each service and repository an interface as it is good for unit testign and reduce coupling between classes

I used the unit of work pattern to handle the database transactions for any entity so  I don't  need to create for each entity the transcation class for it

it starts by the generic repository that access the ticket context class(dbcontext) and  execute the stored procedures

I limit the number of the procedures to the name of procedures  I created in the database for more safety

This genric class is suitable for any model with the valid procedures

If an entity has differnt methods you can create a reposiotry for it and implement the IGenric repository so you can call it from unit of work

The unit of work  class handles the  transactions

so if I have many models you can  replace  the type <T> with it and call the functions defined in the geneirc repository

I used the result pattern for handling any failure and also  confirm if there is a  success

 the service takes the  parameters given for it from the controller  , filter it and validate it then call the methods  of the genric repository using the unit of work
 I also used mapping here to map the result from the parameters to the output

----------------------

TicketManagment layer:
this is the web app project

here you can find controllers that handles the requsts , call the function in the service and returns the response

Extensions class is used to call the service instead of calling them in the setup project and make it dirty with so many calls

mapping folder has class ticket mapping to make any entity related to ticket using automapper

Validators folder:
handle the validations for the request so we don't need  to make many if statemnts in the controller and break the concern of the controller as it is not responsible for any internal business or validations it only take request , call functions and return response

I created an action called Error so if any error they redirect to it with e view displaying the error 
I created the view model folder for requests and responses
I created the folder view and aslo created the home page


 - How to run the application
in visual studio:
in tools->nuget package manager->package manager console
Add-Migration Update_DBClass
update-database

Notes:
I created the home page with a background photo  and added the search bar
it searches by the full name of the ticket