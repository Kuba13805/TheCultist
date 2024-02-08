VAR test_passed = false
-> startConversation

===startConversation===
"Hello there. Can I ask you, if you can help us? older man smiles while staring at you.
    *["Yes, I am always ready to help those in need."] -> agreeToHelpHim
    *["I have other important things to do, old man. I'm sorry."] -> refuseToHelpHim
    
===agreeToHelpHim===
"Thank you, good man. As you can see, we have many problems."
"But you can handle one of them. Oh I am sure you can."
    *["What do you need?"]
"Someone has stolen some precious things from my bag. Food, to be more precisive."
    **["What was stolen?"] -> stolenFood
    **["Maybe you can ask lady Hatroga for some food. I am sure she will help you."] -> HatrogasHelp

===stolenFood===
"Well... two carrots. But not some usual corrots! No! These were blessed by Lucius himself! But do not ask me how I got them. It's a secret."
    *["Two blessed... carrots? It must be a joke.] -> refuseToHelpHim
    *["I will try to find them."] -> startQuest
    *[Try to know something more about blessed carrots. #Persuasion:10] -> blessedCarrots
    
===startQuest===
"Wonderful!" old cleric seems happy that someone finally agreed to help him.
"I will wait for you here. Please, bring my carrots back to me." He starts whispering "The Corruption can find them everywhere." #questStart:testQuestline:2
    *[Don't worry, I will find them.] -> END
    
===HatrogasHelp===
"Oh I asked her to order some of our troops to start looking for my food. I got kicked out of her office, when I explained little more, what was missing."
"They called me a madman and gave some fruits and bread. But these things are useless!"
    *["Lady Hatroga gave you her own food and still you are not happy?"]
        "I don't want her food. I asked her to find things stolen by one of her servants!"
        "Her late husband tried to gather some artefacts of Lucius, Our Beloved Golden Father. I am sure that someone thought that my precious will grant him lady Hatroga's trust and support."
                **["Sound's like it was a very powerful item you had. And it was food? Maybe a magical bread or cucumber?"] -> stolenFood
    *["So explain to me, why your stolen food was so precious? Was it a golden apple?"] -> stolenFood
    
===blessedCarrots===
{
    - test_passed:
        "I see that you are a devoted servant of Lucius. Very well."
        "These carrots were grown at the Golden Palace, where Our Father himself looked for them and blessed all food that came from his garden. They can pure his light into your soul, but only, if you are worthy of his grace."
        "Many has tried to take them from me, but all of them faild. Well, besides one thief."
        "Now you know how important these carrots are and why we have to find them. Can you help me?
            *["If this story is true, then it is my responsibility to keep them safe."] -> startQuest
            *["Only a madman could believe in this story. There are others, more important things to do, old man."] -> refuseToHelpHim
    - not test_passed:
        "Bring the carrots to me and I will tell you their story".
            *[Agree] -> startQuest
            *[Refuse] -> refuseToHelpHim
}

===refuseToHelpHim
"Your eyes will open, when it will be too late to save this world. Remember my words!"
    *[Leave this crazy, old man to his madness] -> END