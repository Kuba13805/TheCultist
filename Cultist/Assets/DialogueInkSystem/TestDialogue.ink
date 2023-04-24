-> start_conversation
=== start_conversation ===
"What do you want?" asked Godfrey.
He looks nervous. Surely something has happened.
    * ["Mind your tongue, slave!"] -> be_cruel
    * ["Where is your master, Godfrey?"] -> where_is_the_master
    * ["Is everything alright?"] -> be_nice
    * [Leave him] -> END


=== be_cruel ===
"You won't tell me what to do!"
"Go to hell"
    * [Punish him]
-> END

=== be_nice ===
"No! Nothing is alright. Have you seen the sky? Something bad is coming."
-> END

=== where_is_the_master ===
"He is inside the temple. Ask for him there."
-> END
