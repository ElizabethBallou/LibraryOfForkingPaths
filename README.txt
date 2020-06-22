What is THE LIBRARY OF FORKING PATHS?

It's a book generator that recombines texts of any kind into endless volumes.

Who can use THE LIBRARY OF FORKING PATHS?

Anyone can use the C# code from this Github repo, as long as you abide by the included MIT License. That means crediting me, Elizabeth, along with my collaborators: Connor Carson, Palmmy Sivarapornsakul, and Mary Georgescu.

How do I use THE LIBRARY OF FORKING PATHS?

Open the scene called ExampleScene. You will see a Game Object called BookManager. The BookController script, which drives the remixing functionality, is attached. There is no need to mess with the code - you can alter the way the LIBRARY OF FORKING PATHS runs just by changing the public variables. Here's a description of them.

SOURCE MATERIALS - A list of TextAssets that contain the text you want to recombine. Save any text you want to use as .txt files and then drag/drop them into this field. Note that BookController will strip the text of line breaks, numbers, and symbols; that is to make it easier when copy/pasting from text with line numbers, footnotes, and citations. However, BookController will not remove the footnotes and citations themselves. It's not that smart!

WORD CHUNK LENGTH - BookController recombines the text into chunks, then prints a random number of text objects per chunk. Word Chunk Length determines how many words are in each chunk. The more words per chunk, the more recognizable the source material will be.

