VAR has_touched_the_stone = false
VAR had_a_vision = false
VAR had_a_snow_figth = false
VAR has_seen_the_golden_palace = false
VAR became_a_god = false
VAR became_a_cult_leader = false
VAR has_escaped_the_tortures = false
VAR snow_fight_lost = false
VAR wanted_to_stop_the_vision = false

VAR test_passed = false
-> start_conversation

===start_conversation===
    A black stone standing in the middle of the square looks at you with a sinister look as you approach it with a slow step.
    As you get closer, you notice thin, blue veins flowing in the black cut sheet.
    "What is it?" you ask yourself in your mind, while staring at the dark abyss.
    *[Touch the black stone]
        ~ has_touched_the_stone = true
        You are pierced by a deadly cold as soon as your fingertips touch the surface of the black stone. You feel the cold creeping deeper and deeper into your body.... and your soul. 
        You hear someone's voice. You can't tell if it sounds familiar or what language it's trying to contact. However, you are sure that it is speaking to you, as if you were standing face to face with its owner.
            **["Who are you?"]
                It says something. It doesn't. He mumbles some filthy sentences in his language, putting them together with strange sounds. You feel someone's gaze on you. He's coming after you.
                    ***[Run]
                        You break into a run, even though you feel you are standing still. You can't see where you're running or what's around you. The voice fades away. You are alone in the midst of emptiness and cold.
                        Suddenly you fall to the ground, knocked down by a strong blow. You want to scream in pain, but your mouth remains closed. After a moment, you pick yourself up, crawling back a few meters so that whatever attacked you won't do it again.  
                            ****[Try to spot the enemy #Perception:12] -> snowFight
    *[Try to see something in the void]
        ~had_a_vision = true
        You see the signs, floating in front of your eyes. There are a lot of them, too many to focus on just one of them for a moment. 
        After a while you realize that you are surrounded by walls of light sandstone and trees planted in huge carved pots. The paved street glistens in the sunlight.
        You feel like you know the place. You know the streets and the walls and the trees.
        **[Recall this place]
            You realize that you are in the heart of Lucius, the Golden City. This is not Alceron, but the area around the palace of the Golden Father. Now you notice the dome of his residence elevated high above the ground and the statue standing in front of it.
            ***[Head to the palace] -> goldenPalace
            ***["This is not true. I need to get out of here." #Power:14] -> escape

===snowFight===
{
    - test_passed:
        ~had_a_snow_figth = true
        At the last moment you manage to see the shape of your opponent. Just before the next strike, you dart to the side to avoid the attack. You break off into a run again. #questComplete:TheBlackStone:1
        You open your eyes. You are standing in front of a black stone in the middle of the square. Only a moment has passed. You swear you just closed your eyes. However, you feel the pain pulsing from where your pursuer hit you. -> END
    - not test_passed:
        ~snow_fight_lost = true
        ~had_a_snow_figth = true
        Another blow sends you two meters back, taking your breath away. You feel how weak you are compared to what you just faced. There's nothing left for you to do but...
        *[...close your eyes]
            Everything fades into darkness. A part of your life, maybe even your soul, flies away. Nevertheless, the chill slowly recedes, leaving you in the darkness. #questComplete:TheBlackStone:1
            You open your eyes. You are standing in front of a black stone in the middle of the square. Only a moment has passed. You swear you just closed your eyes. However, you feel the pain pulsing from where your pursuer hit you. -> END
}

===escape===
{
    - test_passed:
        You snap out of the dark vision that is fading before your eyes. #questComplete:TheBlackStone:1
        You open your eyes. You are standing in front of a black stone in the middle of the square. Only a moment has passed. You swear you just closed your eyes.  -> END
    - not test_passed:
        Something is preventing you from getting your mind out from under its influence. A powerful force hidden in the black stone tightens its fingers around your frail body.
        Life escapes from you. But not enough to kill you. This thing will leave you with life and consciousness to torment you with the nightmares of the moment. #questComplete:TheBlackStone:1
        You open your eyes. You are standing in front of a black stone in the middle of the square. Only a moment has passed. You swear you just closed your eyes. However, you feel the pain pulsing from where your pursuer hit you. -> END
}

===goldenPalace===
~has_seen_the_golden_palace = true
    The huge building towers over the rest of the temple district. It used to be visible from anywhere in the city before the city grew to skyrocketing proportions.
    You are approaching the statue of Lucius, the Golden Father. The statue shines in the sunlight. After a while, it seems to blacken, absorbing the light and suffocating it.
    Lucius the Golden before your eyes turns into Lucius the...
        *[... Liar]
            Yes, Lucius the Liar will be the new master of this city. His web of lies has deeply entrenched itself in the hearts of the residents. -> heresy
        *[... Deceiver]
            He has deceived many to achieve divinity, and now manipulates his worshippers to do his vile will. -> heresy
        *[... Enemy]
            Enemy of the People, Executioner of Husbands, Enemy of the World. He holds all these titles, although he does not admit to them. -> heresy
        *["All these titles fit him."]
            Each of them fits. -> heresy
        
===heresy===
And what will you do to prevent it?
    *["I will lead his enemies against him."]
        To take his place?
            **["Yes"] -> newGod
            **["No"]
                You don't believe in your own words.
                You will deprive him of the throne and become him in the service of your people. 
                ***[It's true. I desire this] -> newGod
                ***["I will not become one!" #Strength:12] -> slaveOfMind
    *["I will bow down to him to destroy his cult from within."] -> traitor
    *[Stop this madness #Strength:12] -> slaveOfMind
===newGod===
~became_a_god = true
You can see the statue of Lucius becoming blurry, only to take on a new look after a while. Now you stand in its place, in greater glory than Lucius ever was.
    *[Praise me]
        The voice of thousands raising prayers in your honor reaches you. They place their lives in your care. They are yours. Yours alone. #questComplete:TheBlackStone:1
        You open your eyes. You feel no cold, no pain, nothing. You are left alone with the thought that, at least for a moment, you were a god. The one and only. -> END
        
===slaveOfMind===
{
    - test_passed:
    ~ wanted_to_stop_the_vision = true
    ~has_escaped_the_tortures = true
        You hit the base of the statue, which trembles with each strike. Walls crack, trees fall. The dome of the Golden Father's palace collapses, destroying Lucius' residence. However, you keep hitting with the fury of a crazed hound.
        With your own hands you tear the false veil of the world. #questComplete:TheBlackStone:1
        Your faithful become silent and you hear only the echo of their prayers. When you open your eyes, you look deep into the refuse of the black stone that greets you as if you were a friend. -> END
    - not test_passed:
    ~ wanted_to_stop_the_vision = true
        You are pathetic, just like Lucius! You should not turn your back on Lust. I will show you the truth.
        Thousands of voices scream in your head in horror and despair. You feel their pain as you fall to your knees. They beg for help, for deliverance from the flames and death. It was sent upon them by the one you so stubbornly refuse to deny! #questComplete:TheBlackStone:1
        As time goes by, the voices fall silent. You rise to your feet, and when you open your eyes, you are again standing in front of a black stone in the middle of the square. -> END
}

===traitor===
~became_a_cult_leader = true
You will infuse venom into an unhealed wound. You defile his work and the work of his followers. You will become his zealous servant to stab him in the back with a dagger.
Under the guise of darkness, you will worship the light of the people.
You see yourself surrounded by people in bloody robes. On their lips are the words preached earlier by you and your acolytes. You have blurred the clouds and given them light. #questComplete:TheBlackStone:1
Your faithful become silent and you hear only the echo of their prayers. When you open your eyes, you look deep into the refuse of the black stone that greets you as if you were a friend. -> END