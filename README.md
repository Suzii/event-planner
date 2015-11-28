# event-planner
<h1>FI MUNI PV247</h1>

<h3>New MDF file:</h3>
  Server Explorer > Data Connections -|- Add Connection
    Data source: Microsoft SQL Server Database File (SqlClient)
    Database file name - Browse -> PATH_TO_WEB_PROJECT/App_Data/EventPlannerDB.mdf
    Use Windows Authentication
    OK
    
In case of not created db in mdf file, type "update-database" in Package Manager Console.

<h2>Event planner ERD</h2>

![ERD](https://github.com/tomaskristof/event-planner/blob/master/Docs/ERD/erd.png)

<h2>Coding rules</h2>

```C#
using System;

public class CodingRules
{
    // constants are written using uppercase and underscore as a separator
    public const string NAME_CONSTANT = "Someone"; 

    // private members of the class are prefixed with an uderscore
    private string _field;    

    // public properties are written using UpperCamelCase
    public int MyProperty { get; set; }    

    // all the method names use UpperCamelCase
    public string Hello(string name)
    {
        // always use blocks, even for one line
        if (name == null)
        {
            throw new ArgumentNullException();
        }

        // variables use lowerCamelCase
        var myName = name;

        return myName;
    }
}
```
