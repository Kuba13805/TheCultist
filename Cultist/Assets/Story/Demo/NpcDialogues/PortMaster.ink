VAR test_passed = false
-> Start

===Start===
As soon as you approach a man, he flinches nervously. Drops of sweat glaze over his face as he looks at you with tired eyes. #questComplete:InvestigateLowerCityPort:1
"You shouldn't be here. The port is closed until further notice."
*["I'm here on a murder case. I want to help solve it."]
"Help? Help. If you really want to get involved, go ahead."
"Jack was new here. He had the potential to take my place someday. He meted out the ultimate punishment for that."
**["Punishment? Are you suggesting that someone wanted to get rid of him because of.... you?"]
"I know this. Someone wants to get rid of me and they used Jack to do it. What does it look like? The old official fears a younger, better successor. It's obvious that suspicion falls on me."
***["Do you have someone in mind?"]
"Ralph."
"He has never liked me and because of that he is looking for a way to remove me. I once caught him sneaking goods. Maybe that pushed him to accuse me? And maybe also to kill Jack."
****[Take a closer look at him #Perception:12] -> deeperQuestions

===deeperQuestions===
{
    - test_passed:
    You perceive the weariness and nerves that are tearing this man apart from the inside. However, there is something else: uncertainty. -> endConv
    - not test_passed:
        You don't see anything unusual about him. An ordinary aging clerk. -> endConv
}

===endConv===
"Talk to my people. With me, they don't want to talk. Maybe you'll have better luck." #questStart:InvestigateLowerCityPort:2
*["If you are innocent, I will try to prove it."] -> END
*["I will. And I'll get to the truth."] ->END