VAR has_touched_the_stone = false
VAR had_a_vision = false
VAR had_a_snow_fight = false
VAR has_seen_the_golden_palace = false
VAR became_a_god = false
VAR became_a_cult_leader = false
VAR has_escaped_the_tortures = false
VAR snow_fight_lost = false
VAR wanted_to_stop_the_vision = false

VAR test_passed = false
-> startConversation

===startConversation===
"Are you okay? You were standing there and all of a sudden it looked like your soul had escaped from your body."
Without asking, the professor walks up to you and places his hand on your forehead.
"Temperature normal, no excessive sweating, eyes slightly bloodshot."
    *[Push his hand away]
        Wendell does not look happy. He seems to quickly understand his mistake and you can see the understanding on his face.
        "Forgive me. It's a habit from work. Usually my patients don't have a say when it comes to the examination." He starts writing down his observations in a notebook.
        Cragan approaches you again, but this time he keeps his hands to himself.
        "Let us proceed." -> continuedResearch
    *[Let him continue] -> continuedResearch
        =continuedResearch
        "No external injuries. Slightly faster breathing." -> physicalExamination
        =physicalExamination
        He writes something in his notes. "Would you like to add anything?"
        *{has_touched_the_stone}["I'm a little cold."]
            "I felt the same when I approached the stone myself. A chill different from that during the winter. More ominous and piercing." -> physicalExamination
        *{had_a_snow_fight}["I feel like someone punched me in the stomach."]
            "Internal injuries? An unprecedented symptom until now. Perhaps a defensive reaction?" -> physicalExamination
        *{wanted_to_stop_the_vision}{not has_escaped_the_tortures}["My head is throbbing, as if something is about to burst it."]
            "Contact with a mystical object can end like this. However, I doubt that something similar will happen to you. The pain should subside by the evening." -> physicalExamination
        *["That's all."] -> experiment

===experiment===
"Now that the research issues are behind us, let's move on to what you saw or experienced after contact with the stone. Tell me everything, don't leave out details."
    *{has_touched_the_stone}["When I touched the stone, I felt cold. It was like it was seeping in."] -> touch
    *{had_a_vision}["I looked at the reflection and into the void. After a while, I saw signs floating in front of me and..."] -> vision
    
===touch===
The professor takes notes on everything you say. At the same time, you can see on his face that he has experienced something similar. 
    *["I heard someone's voice and asked who he was. In response, I heard a strange language that I didn't understand."]
        ...
            **["Suddenly I felt something coming after me. I started to run, but something knocked me down after a while."]
                "That would explain the pain in the abdominal area. Please go on."
                    ***{not snow_fight_lost}["This thing attacked me. I managed to dodge another blow and escaped. Then I opened my eyes."]
                            "So you were very lucky. For me you stood still, staring all the time at the stone. Just where did the pain come from if nothing attacked you in the real world?"
                            The professor begins to wonder. He strokes his white beard, wrinkling his forehead and eyebrows. -> conclusion
                    ***{snow_fight_lost}["The second blow came. I felt myself slipping to the ground. I closed my eyes and felt my life and soul flying out of me."]
                        "It sounds ... awful. Forgive me for exposing you to something like that."
                        The professor removes his glasses and with the fingers of his left hand begins to slowly massage his closed eyelids. -> conclusion
===vision===
...
    *["The Golden Palace"]
        Professor Cragan opens his eyes wider. He wants to say something, but the words are stuck in his throat.
        "The stone showed you the very heart of Lucius. However, it has been stuck here for centuries, so.... would have to have access to your memories and thoughts. This is..."
            **["Sick."]
                "Well... Yes... Sick. You're right," he replies confusedly. "What happened next?" -> visionOfFuture
            **["Fascinating and terrifying at the same time".]
                "If our theory is confirmed, further research will be needed to discover the whole truth! This is truly remarkable!"
                "What was next? Please speak up." -> visionOfFuture
            **["Impossible. Unless..."]
                "Perhaps the old texts are true. Demons were in fact locked in these stones. Their power was limited, but strong enough to have an effect on those who would approach their prison."
                    "Did the demon show you anything else?" -> visionOfFuture
                
=visionOfFuture
*{has_seen_the_golden_palace}["I saw a statue of black stone standing in front of the palace. And then..."]
    ...
        **{became_a_god}["...I became a god in the place of Lucius."]
            "He deceives us and entices us with promises of greatness and power."
            "It's a demon. Of that I am sure." -> conclusion
        **{became_a_cult_leader}["...I have become a cult leader who will betray the belief in the Golden Father."]
            "Betrayal is only what would shake faith in Lucius to its very foundations. No wonder you were shown this image. Demons love chaos and betrayal." -> conclusion
        **{not became_a_god}{not became_a_cult_leader}{has_escaped_the_tortures}["...I escaped when the opportunity arose, when I regained some of my consciousness."]
            "If you had stayed there a little longer, perhaps we would have learnt more."
            "But you would be risking your life. It was a good thing you freed yourself from his influence." -> conclusion
        **{not became_a_god}{not became_a_cult_leader}{not has_escaped_the_tortures}["... I felt the cry of a thousand voices in my head. I fell to my knees, begging for death."]
            "He can't inflict a wound on the body, so he does it to consciousness. Soon the pain should subside." -> conclusion
*{not has_seen_the_golden_palace}["I freed myself from the vision. I didn't want to see what he had prepared for me."]
    "The filthy image would probably be stuck in your thoughts for years. Nothing would be the same again as it was before." -> conclusion

===conclusion===
"There is a demon trapped inside this stone. And not just any kind, I'm guessing. Personally, I don't know anyone we can trust with entrusting such valuable knowledge, so we have to decide for ourselves what to do with it." #questComplete:TheBlackStone:2
The professor goes around the stone, looking carefully at its bases. He keeps his distance, however."
"We cannot destroy him and the stone. However, we can try to imprison him better or persuade Lady Calpernia to have her people constantly guard the stone to protect it."
"You were the one who undertook the attempt to examine the stone, so it is left to you to make the final decision." -> decision
=decision
    *["We have to try and destroy the stone."]
        "If we don't succeed, we will let the demon loose! Think how powerful it will be outside the cage!"
            **["I was the one who measured myself against the demon and I will be the one to decide. You are with me or..." #Intimidation:8] -> intimidateProfessor
            **["You may be right. Another way has to be found."] -> decision
    *["Security needs to be strengthened. This thing can't get out."]
        "I have some books in my office that will allow us to create protective signs around the stone. Meet me there tonight." -> END
    *["I might be able to get Calpernia to put some soldiers on guard."]
        "May she be more eager to talk to you. I will pray to the Father for your success." -> END
    
===intimidateProfessor===
{
    - test_passed:
        After a moment's silence, Craig agrees with you nodding his head.
        "Meet me in my office in the evening. There we will discuss ways to destroy the stone." -> END
    - not test_passed:
        "I cannot allow you to commit such a reckless act. Please hear me out and let's work together to figure out how to deal with this thing."
            *["Let it be. Let's try it your way."] -> conclusion.decision
}   