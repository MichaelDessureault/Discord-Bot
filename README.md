# Discord-Bot
# Created by Michael Dessureault

Features: 
- Uses Giphy Api to get gifs and stickers based on users input
- Uses Twitter Api to get live updates from accounts that are being followed, the updates get posted into the desired channel
- Key word dectection, has a dictionary that the user can create through discord.
    - These key words can be for Gifs/Stickers or a phase EG: Key Word is "hello" and the output is "Hello World!", when "hello" is typed in the chat if key word checker is enabled it will print the output "Hello World!" into the chat that the key word was typed in.
    
Setup Procress:  (All config files are found in the Resources Folder)
  - Step 1: Open discordconfig.json file
    - Populate the token
    - Populate the cmdPrefix.
  - Step 2: Open giphyconfig.json file
    - Populate the key
  - Step 3: Open twitterconfig.json file
    - Populate the custer_key
    - Populate the customer_key_secret
    - Populate the access_token
    - Populate the access_token_secret
    - Populate the channel id that the bot will post messages too.
