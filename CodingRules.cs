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
