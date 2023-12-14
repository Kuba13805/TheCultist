VAR test_passed = false
-> Start

===Start===
"Did you just come to disturb me?"
The one-eyed man seems annoyed by your presence. He doesn't even look at you.
*["I'm here on a murder case."] -> Questions
*["It will only take a moment. Tell all about Jack's murder."] -> Questions

===Questions===
"I told the boss everything I know. Jack snooped where he shouldn't have. He knew what he was signing up for."
*["What did he sniff out?"]
    "Such information is not free. I could tell you, but then I would lose valuable knowledge."
    **["You better tell me. You'll soon lose your jobs if the port doesn't resume transport." #Persuasion:10] -> Persuasion
    **["I'm not going to ask. Either you tell me, or we have two murders to solve." #Intimidation:10] -> Intimidation
    **["I will owe you a favor.]
        "A favor with someone from the upper city? Sounds good." -> QuestHelp
    **["I can manage without your help."]
        "And I wish you that. The faster you do your job, the faster I'll get back to mine." -> END
    
===Persuasion===
{
    - test_passed:
        "What, no one told us that they could close the port for so long."
        "Be it to you. I'll tell you what I know. Then you'll go away and let me work." -> QuestHelp
    - not test_passed:
    "No one dares to do that. Go disturb someone else." -> END
}
===Intimidation===
{
    - test_passed:
        "Relax. There is no need for that. Let you be. I'll tell you what I know. -> QuestHelp
    - not test_passed:
    "Be careful. You don't want to mess with me. Let me work in peace or you'll be out of this port faster than you got here." -> END
}
===QuestHelp===
"Jack witnessed a transport that was destined for smugglers. The boss is in their debt and must provide them with everything they need."
"Ralph had been wanting to reveal the truth for some time, but he knew that no one would believe him without evidence."
"Or someone who, in the course of the investigation, comes to that conclusion himself." 
"If it was Ralph who killed Jack, he did it to get rid of the boss. And if he had nothing to do with it.... #questComplete:InvestigateLowerCityPort:2
*["...The blame falls on your boss. He is the one who helped the smugglers."]
"Here's the thing. Think about what you heard here and make the right decision. By the way, you can check the crates in the port. You might find something useful there." #questStart:InvestigateLowerCityPort:3
"Recently there was someone suspicious hanging around. Maybe he left some traces."
The man returns to sweeping. -> END