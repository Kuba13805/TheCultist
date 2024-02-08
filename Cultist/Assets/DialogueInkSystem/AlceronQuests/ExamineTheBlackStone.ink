VAR test_passed = false
-> startConversation

===startConversation===
You approach an older man dressed in an elegant, albeit worn coat. His snow-white hair adds to his friendly countenance.
He doesn't seem to notice you. After a moment, he takes his nose out of his notebook and corrects the silver glasses on his nose. 
"Oh sorry most sincerely." embarrassment appears on his face. "I'm already used to being ignored. No one here seems to be fond of people of my kind." he adds with a laugh.
"My name is Wendell Cragan and I am a professor at the University of Oxkard. I hope you were not sent here because of me. Lady Calpernia was not pleased with my visit to her new kingdom."
    *["Nice to meet you, professor. I am here solely in my own interest."]
        "I understand. Many today live only for themselves. One can't be surprised at them, after all, dark times have come, right?"
        Cragan looks at the black stone at which you are talking. 
            **["Perhaps I could help with something?"]
                "Perhaps there is something I might need help with from someone who is not afraid to take risks." -> helpInNeed
            **[Take a look at the black stone] -> strangeStone
    *["Yes, Lady Calpernia sent me. She wishes to know a little more about the reasons for your visit."]
        "It will be hard to explain my research to her when she is not with us. I'm not going to explain it to someone who might mistakenly get something wrong. "
        "Unless she wanted to help me. Then I would be more willing to explain what my work is about."
            **["Fine, I'll help you."] -> helpInNeed
            **["My mistress is not going to help you."]
                "Then don't bother me. If she changes her mind, I'll be here, by the stone." -> END


===strangeStone===
From a distance it looks like an ordinary polished stone. You don't approach it, but you seem to feel a slight chill carried by the wind from its side.
    *[Focus on the black abyss of stone]
        You stare into nothingness. It is as if there is a void inside the black stone. For a moment you have the feeling that something is oozing the smooth surface like the surface of a lake.
        You feel like approaching him. A moment later, the feeling passes. -> strangeStone
    *["Professor? Are you all right?"]
        "Yes, forgive me, I was thinking." Cragan takes off his silver glasses and rubs his tired eyes. "Where were we?"
            **["Perhaps I could help with something?"]
                "Perhaps there is something I might need help with from someone who is not afraid to take risks." -> helpInNeed
===helpInNeed===
"You see, I have been researching stones similar to this one for some time. There are several in Lucius alone, and these are just the ones we know about."
"No one knows their history or destiny. We know as little about them as at least our greatest enemies, the Demons of Corruption and Spite." -> storyOfTheStones
= storyOfTheStones
    *["There's more?"]
        "So far, I have been able to examine three of the seven found in the city. This one would be the fourth, if not..." The professor suspends his gaze again on the stone, as if trying to drown his gaze in the reflection of its surface.
        "There's something about this that I haven't encountered in previous research. When I try to approach it and touch the surface, I feel a terrifying cold climbing from my feet all over my body."
        "If you want to help, could you try for me? It will be fascinating to watch what happens during the contact with the stone from the observer's side."
        **[Agree to help]
            "Thank you." The professor takes a few steps back. "When you are ready, approach the stone. I'll be watching you the whole time, and I'll help if anything bad happens." #questStart:TheBlackStone:1
            Despite his assurances, you feel uneasy. But also excitement. -> END
        **[Decline his request]
            "I understand your concerns." Despite his refusal, Wendell seems determined to investigate the stone further.
            "Should you change your mind, you will find me here." -> END
    *[Recall if you know anything about these stones #Wisdom:18] -> oldWisdom
    *[Show off your knowledge about demons #Occultism:12] -> demonHunter
    
===oldWisdom===
{
    - test_passed:
        During your life, you've heard of something with similar structures to the one you're facing. According to some, they are as old as the world, while others claim that they were created to lock up the most powerful of demons.
        Then there are those who worship them secretly, believing them to be the primordial force that created the world. However, those are dwindling with each hunt by Lucius' followers. -> helpInNeed.storyOfTheStones
    - not test_passed:
    There are many things you still have to learn, and the history of these stones is one of them. -> helpInNeed.storyOfTheStones
}

===demonHunter===
{
    - not test_passed:
    Maybe you could share more, but do you have anything to share? -> helpInNeed.storyOfTheStones
    - test_passed:
        You recall everything you know about the forces mentioned by Wendell.
        *["We may know little about them, but enough to destroy them. Many have described their species, powers and intentions."]
            "The Great Book of Demons and Heretics is a work both magnificent and terrifying. It opens one's eyes to what surrounds us and what must be opposed."
            "So I'm dealing with someone who knows more about them."
                **["I was given the opportunity to face some demons. At the very beginning of my story."]
                "Admirable. I'm surprised that someone like that didn't join the Temple of the Golden Father. I feel you would have gone far." -> helpInNeed.storyOfTheStones
}