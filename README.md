Name: Spencer Lynn
Section: CSCI 1260-002
Track: River City Supply
Langauge: C#

Press run and console should display what is requested!

Situation where this is the right call to be equal: 'dairy' and 'dairy3' get set equal from the hashset 
so in a real world setting (serial #s, recording license plates at different times, etc) would be treated alike.

Situation where this is a bug: items can be lost in other settings, I'm not entirely sure but I think adding ValueOnHand to HashCode.Combine 
would force them to not store them as separate "things".
