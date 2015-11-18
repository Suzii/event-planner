# event-planner
FI MUNI PV247

New MDF file:
  Server Explorer > Data Connections -|- Add Connection
    Data source: Microsoft SQL Server Database File (SqlClient)
    Database file name - Browse -> PATH_TO_WEB_PROJECT/App_Data/EventPlannerDB.mdf
    Use Windows Authentication
    OK
    
In case of not created db in mdf file, type "update-database" in Package Manager Console.

Event planner ERD

![ERD](https://github.com/tomaskristof/event-planner/blob/master/Docs/ERD/erd.png)
