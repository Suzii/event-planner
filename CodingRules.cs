using System;

public class CodingRules
{
    public const string NAME_CONSTANT = "Someone";  //constants are written using uppercase and underscore as a separator

    private string _field;    //private members of the class are prefixed with an uderscore

    public int MyProperty { get; set; }    //public properties are written using UpperCamelCase

    //all the method names use UpperCamelCase
    public string Hello(string name)
    {
        //always use blocks, even for one line
        if (name == null)
        {
            throw new ArgumentNullException();
        }

        //variables use lowerCamelCase
        var myName = name;

        return myName;
    }
}
