VAR knows_where_is_master = false
VAR knows_about_the_sky = false
VAR test_passed = false
-> start_conversation
=== start_conversation ===
"What do you want?" 
He stops writing as soon as you approach him.
    * ["Mind your tongue, slave!"] -> be_cruel 
    + ["Where is  your master, Godfrey?"] -> where_is_the_master
    + ["Is everything alright?"] -> be_nice
    + {knows_where_is_master} [Go to see his master] -> END


=== be_cruel ===
"You won't tell me what to do!" 
"Go to hell"
    * [Punish him #Power:15] -> punish

=== punish ===
{
    - test_passed: #ignore
        His eyes turn dark, as you start whispering the dark incantation.
        Godfrey screams in pain, but it is already too late.
        At the end, you catch a glimps of his memories. -> godfrey_memories
    - not test_passed: #ignore
        You felt something strange, when your energy touched Godfrey. Something... corupted.
        "Your magic tricks won't work here, Master..."
            * [Leave him to his darkness] 
            He starts writing again, while you leave him alone. -> END
}
    
=== be_nice ===
"No! Nothing is alright. Have you seen the sky? Something bad is coming."
His hands begin to tremble as his voice cracks with each word.
    *[Try to calm him down #Persuasion:10] -> calm_Godfrey_down
    *["Tell me, where is your master."] -> where_is_the_master
=== calm_Godfrey_down ===
{
    - not test_passed: #ignore 
        "How can I stay calm? Are you blind or what?"
        Godfrey looks up at the dark sky. You can see the fear in his eyes and the desire to hide as deep as possible. 
            + {knows_about_the_sky} [Tell him about the sky] -> the_sky
            + ["Mind your tongue, slave!"] -> be_cruel
    - test_passed: #ignore
        Godfrey calms down a bit. His hands stop shaking and his voice stops cracking. You can see the shame in his eyes from the earlier outburst.
        "I am sorry and... I... You. You are here because of master Trembly, am I right?
            * ["Just tell me where he is."] -> where_is_the_master
            * ["Yes, Godfrey. We need his help."]
                ~ knows_where_is_master = true
                "He is in the temple. Something important has happened. Maybe they found a way to make the sky healthy again? I hope so." #questComplete:1
                ** ["Take a break. We've all been tired lately."]
                    "Yes. I will sit for a moment and watch the clouds.
                        -> END
                ** ["You are a good servant. I have to go."]
                    "Thank you master. May the Golden Lord bless you!"  -> END
                    
}

=== the_sky ===
"I knew that these demons would return one day!"
"Go to master Trembly and tell him about the sky"
    * ["Where is your master?"] -> where_is_the_master
=== where_is_the_master ===
~ knows_where_is_master = true
"He is inside the temple. Ask for him there." #questComplete:1
    *["Thank you, Godfrey"]
        He starts writing again, while you leave him alone. -> END
    

=== godfrey_memories ===
~ knows_where_is_master = true
*[Search for his master]
    You see master Trembly in the great halls of local temple. Now you know where you can find him. #questComplete:1
    -> END