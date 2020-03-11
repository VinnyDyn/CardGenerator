# CDS Card Generator for Teams

Based on CDS records, call a Action using the Power Automate to post an adaptive card on Teams.

![alt text](https://github.com/VinnyDyn/CardGenerator/blob/master/Images/demo.gif)

### Features
- The card will be generated based on parameter 'EntityAttributes' respecting the sequence.
- The parameter 'EntityAttributes' suports: String, Numbers, DateTime, Booleans, Money, OptionSets and Lookups.
- The parameter 'AppId' is optional. If it has value, when the user click on the button he will be redirect to specific Model Driven App.

### Limitations
- The 'Open record' button text can be changed. (yet).

### Prerequisites
- Install de solution (VinnyBControls 1.4.3.0^)

### How to Implement
<img src="https://github.com/VinnyDyn/CardGenerator/blob/master/Images/power-automate-config-01.png" width="400" height="450"/>

Step | Description
------------- | -------------
0 | Create a Power Automate (Flow) through the Solution (https://make.powerapps.com/environments/{solutionid})
1  | Define a trigger.
2  | Add a action -> Common Data Service (Current Enviroment) : Perform Unbound Action.
2 (Action Name)  | Select the option 'vnb_GenerateCard'
2 (AppId)  | Represents the Model-Driven App Id (Guid)
2 (Entity Name)  | Entity Logical Name (account, contact, opportunity, etc..)
2 (Entity Id)  | Entity Id (Guid)
2 (Entity Attributes)  | Attributes Logical Names splitted by comma
3  | The action will return 3 parameters: Sucess, Trace and Card. This condition verify if the execution was a Sucess (true)

<img src="https://github.com/VinnyDyn/CardGenerator/blob/master/Images/power-automate-config-02.png" height="275"/>

Step | Description
------------- | -------------
4 (Sucess) | Add a action -> Teams : Post a Adaptive Card as a Bot on Channel.
4 (Config) | Select a Team, Channel and insert the output of the step 2 (Card).

### Ready to use
The [managed](https://github.com/VinnyDyn/StatusReasonKanban/releases/download/1.4.3/VinnyBControls_1_4_3_0_managed.zip) solution is ideal for non developers. Import and use.

### References
https://adaptivecards.io/designer
