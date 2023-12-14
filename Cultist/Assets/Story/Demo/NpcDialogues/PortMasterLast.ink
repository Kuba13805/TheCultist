VAR ralph_is_guilty = false
VAR port_master_is_guilty = false
VAR no_one_is_guilty = false
-> Start

===Start===
"Good news? I hope you were able to establish something about Jack."
*["I found the blade with which the boy was killed."]
    You show him a bloody dagger. You see the guilt and despair painted on the man's face.
    "It's my fault. I thought I would be able to protect my employees and pay off my debt to the smugglers." #questComplete:InvestigateLowerCityPort:4
    "I'm not going to explain myself. It's too late for that." #questStart:InvestigateLowerCityPort:5
    "But I can ask you for one thing. Let's acknowledge that Ralph is to blame. He is the one who killed Jack in an effort to protect his position." -> Questions
    =Questions
        **["It makes no difference which of you will answer for Jack's death. What matters is that the port works."]
            "Well, that's just it. Without me, the port will be closed until my successor is elected. Food, medicine, warm clothes. People will be left without them for weeks." -> Decision
        **["Why should I help you?"]
            "I am the only one who can run the port without delays. People will get what they need to survive. Think of them, not me." -> Decision
        **["You don't have the money to buy me."]
            "I may not be able to pay you, but I can promise you that I will whisper a few words to the smugglers so that you will have respect from them. Who knows what you could use them for." -> Decision
        **["Do you want to prosecute Ralph? I won't allow it!"]
            "You are condemning many lives in the city to death with this. They will be deprived of medicine and food. But the important thing is that one fool will answer for his mistake, won't he?" -> Decision
            
===Decision===
You know that this man's future is in your hands. No, not just his. Ralph, the people of the lower city, the workers of the port. You feel the weight of that responsibility.
You have to choose.
*[Prosecute the port chief. He must answer for what he brought upon his subordinates.]
~ port_master_is_guilty = true
The man says nothing. He just looks at the floor. He knows what awaits him.
You leave it. You have to write up a report and give it to the city guard. He won't escape anyway. He has nowhere to go. -> Ending
*[Blame Ralph for Jack's death. This sacrifice will save many lives in the city and the port.]
~ ralph_is_guilty = true
"Forgive me for forcing you to make this choice. It is good that it turned out to be the right one."
The man's face shows the relief and peace he has been craving for days. Now it's time to complete your duties. -> Ending
*[The real killer escaped long ago. You can't prosecute someone who hasn't shed blood. ]
~ no_one_is_guilty = true
The man looks at you puzzled. After a moment, he smiles, finally recognizing your wisdom in making this decision.
"If necessary, I will help find these smugglers. You can count on me." -> Ending

===Ending===
You have fulfilled your task. You can go home.  #questComplete:InvestigateLowerCityPort:5
Finally. -> END