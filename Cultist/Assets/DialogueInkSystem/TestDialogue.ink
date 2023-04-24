-> start_conversation
=== start_conversation ===
"What do you want?"
He looks nervous. Surely something has happened.
    * ["Mind your tongue, slave!"] -> be_cruel
    * ["Where is your master, Godfrey?"] -> where_is_the_master
    * ["Is everything alright?"] -> be_nice
    * [Leave him] -> END


=== be_cruel ===
"You won't tell me what to do!"
"Go to hell"
    * [Punish him]
    His eyes turn dark, as you start whispering the dark incantation.
    Godfrey screams in pain, but it is already too late.
    At the end, you catch a glimps of his memories.
-> godfrey_memories

=== be_nice ===
"No! Nothing is alright. Have you seen the sky? Something bad is coming."
    *[Try to calm him down] -> be_cruel
    *["Tell me, where is your master"] -> where_is_the_master

=== where_is_the_master ===
"He is inside the temple. Ask for him there."
    *["Thank you, Godfrey"]
-> END

=== godfrey_memories ===
*[Search for his master]
You see master Trembly in the great halls of local temple. Now you know where you can find him.
-> END
