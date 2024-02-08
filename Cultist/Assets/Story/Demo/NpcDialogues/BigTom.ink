VAR test_passed = false
->Start

===Start===
You look at the giant, who effortlessly rearranges one crate after another. When he notices you, he bursts out laughing.
"Another skinny? The boss must have a lot of fun sending people like you to us. He wouldn't be amused if he took on the job himself with the likes of you to help."
"Come on, don't be offended. Big Tom just wanted to make a joke. Well, get to work. These crates won't move by themselves!"
*["I am not an employee. I'm looking for a murderer."]
    "Finally someone took care of it. Good. The boss kept claiming he was working on a solution. At least one sentence out of his mouth was true."
    **["Why don't you tell me everything you know about the murder?"] -> JackInvestigation
*[Help him move crates]
    The big man hands you one of the crates. You feel the pain tear your arms and try to knock you to the ground.
    **[Try to stand and move the box #Strength:10] -> Work
    **[Let it go. You're not here to carry a crate."]
        The crate falls to the ground with a bang. You hear something inside crack into tiny pieces. The big man is not happy."
    However, after a while he bursts out laughing and slaps you on the shoulder in a friendly manner.
    "It happens to everyone. Jack dropped three cases of tea into the water on the first day. He has a worthy replacement." -> Questions

===BossTruth===
{
    - test_passed:
        The man calms down. He begins to trust you. 
        "The boss does illegal business with smugglers in the city. I saw him checking something in the crates from the last delivery. Earlier he called Jack into his office. After that, I didn't see him again." #questComplete:InvestigateLowerCityPort:2
        "I don't know if the killer is the boss, but if so, arrange it so that no one gets hurt." #questStart:InvestigateLowerCityPort:3
        The big man picks up the crate and walks away. -> END
    - not test_passed:
        "I said everything I know. Leave me alone, I have a lot of work to do." -> END
    
}

===Work===
{
    - test_passed:
        You pull yourself together and carry the box to the indicated place. A smile appears on the big man's face.
        "Frail and weak, but sturdy. Good, we need such." -> Questions
    - not test_passed:
    The crate falls to the ground with a bang. You hear something inside crack into tiny pieces. The big man is not happy."
    However, after a while he bursts out laughing and slaps you on the shoulder in a friendly manner.
    "It happens to everyone. Jack dropped three cases of tea into the water on the first day. He has a worthy replacement." -> Questions
}

===Questions===
I'm Big Tom. Did they send you to replace Jack?"
    *["Actually, I'm here to investigate his death. Will you tell me what you know?"] -> JackInvestigation
    
===JackInvestigation===
The big man puts the crate on the ground and sits down on it.
        "Any of us could have done it. Jack would have gotten under everyone's skin sooner or later. That's the kind of person he was. From day one he tried to do everything to avoid work, but he hankered for money like no one else."
        "The day before he died, I overheard him threatening someone to reveal the truth about the operation of the port. I don't have the faintest idea what he was talking about, but to my knowledge it was something related to illegal shipments."
        ***["You know about the illegal business and you keep quiet? You are very loyal to your superior."]
            "I'm not looking for trouble. I just want to do my own thing and make a living. If I said something, they would quickly find another to take my place." #questComplete:InvestigateLowerCityPort:2
            "If you want to snoop further, check out the latest transports, and maybe you'll learn something. Just don't come back to me with it." #questStart:InvestigateLowerCityPort:3
            The big man picks up the crate and walks away. -> END
        ***["Illegal business in the port? Impossible."]
            "You can make quite a bit of money on this. Other than me, I think only Ralph knows about them. He once warned our boss that everyone would regret it if the truth came out." #questComplete:InvestigateLowerCityPort:2
            ****["Maybe Ralph killed Jack?"]
                "Maybe he's capable of it. Who knows. If you care about the truth, search the recently brought crates. Just don't reveal who you know it from." #questStart:InvestigateLowerCityPort:3
            The big man picks up the crate and walks away. -> END
            ****["Maybe it was your boss who ordered Jack's murder?"]
                The man croaks and avoids your gaze. Your words have caused a stir in him.
                *****["I want to help. I won't if you don't tell me the truth." #Persuasion:15] -> BossTruth
                *****["Tell me anything that can help and I'll leave you alone."]
                    "Check the latest deliveries. That's all I can say." #questComplete:InvestigateLowerCityPort:2
                    "Remember, we didn't talk." #questStart:InvestigateLowerCityPort:3
                    The big man picks up the crate and walks away. -> END