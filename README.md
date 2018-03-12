# Discord-Bot
# Created by Michael Dessureault

Features: 
- Uses Giphy Api to get gifs and stickers based on users input
- Uses Twitter Api to get live updates from accounts that are being followed, the updates get posted into the desired channel
- Key word dectection, has a dictionary that the user can create through discord.
    - These key words can be for Gifs/Stickers or a phase EG: Key Word is "hello" and the output is "Hello World!", when "hello" is typed in the chat if key word checker is enabled it will print the output "Hello World!" into the chat that the key word was typed in.
    
Setup Procress:  (All config files are found in the Resources Folder)
  - Step 1: Open discordconfig.json file (https://discordapp.com/developers/docs/intro click "My Apps" and create one for token) 
    - Populate the token
    - Populate the cmdPrefix.
  - Step 2: Open giphyconfig.json file  (https://developers.giphy.com/ click "Create an App" for key)
    - Populate the key
  - Step 3: Open twitterconfig.json file (https://apps.twitter.com/ click "Create an App" for keys)
    - Populate the custer_key
    - Populate the customer_key_secret
    - Populate the access_token
    - Populate the access_token_secret
    - Populate the channel id that the bot will post messages too.
    
To get the channel id have discord in developer mode (Settings > Appearance > Advanced enabled developer mode) then right click on the channel you want and copy the id.
