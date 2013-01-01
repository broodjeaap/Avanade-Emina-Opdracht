enquetes = range(5)
questions = range(5)
possibleAnswers = range(3)

f = open('query.sql', 'w')

f.write("SET IDENTITY_INSERT [dbo].[Enquetes] ON\n")
for x in enquetes:
    f.write("INSERT INTO [dbo].[Enquetes] ([EnqueteID], [Name], [Description], [StartDate], [EndDate]) VALUES ")
    f.write("("+str(x+1)+", N'Enquete "+str(x+1)+"', N'Enquete "+str(x+1)+" is de "+str(x+1)+"e Enquete in de database', N'2013-01-01 00:00:00', N'2013-02-02 00:00:00')\n")
    
f.write("SET IDENTITY_INSERT [dbo].[Enquetes] OFF\n\n")

f.write("SET IDENTITY_INSERT [dbo].[Questions] ON\n")
count = 1
for x in enquetes:
    for y in questions:
        f.write("INSERT INTO [dbo].[Questions] ([QuestionID], [EnqueteID], [Text], [type]) VALUES ("+str(count)+", "+str(x+1)+", N'Enquete "+str(x+1)+", Question "+str(y+1)+"', 0)\n")
        count += 1

f.write("SET IDENTITY_INSERT [dbo].[Questions] OFF\n\n")

f.write("SET IDENTITY_INSERT [dbo].[PossibleAnswers] ON\n")
count = 1
qCount = 1
for x in enquetes:
    for y in questions:
        for z in possibleAnswers:
            f.write("INSERT INTO [dbo].[PossibleAnswers] ([PossibleAnswerID], [QuestionID], [Text]) VALUES ("+str(count)+", "+str(qCount)+", N'Enquete "+str(x+1)+", Question "+str(y+1)+", PossibleAnswer "+str(z+1)+"')\n")
            count += 1
        qCount += 1
f.write("SET IDENTITY_INSERT [dbo].[PossibleAnswers] OFF")

f.close()
