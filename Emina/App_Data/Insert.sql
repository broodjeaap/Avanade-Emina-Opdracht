SET IDENTITY_INSERT [dbo].[Enquetes] ON
INSERT INTO [dbo].[Enquetes] ([EnqueteID], [Name], [Description], [StartDate], [EndDate]) VALUES (1, N'Enquete 1', N'Enquete 1 is de eerste Enquete in de database', N'2013-01-01 00:00:00', N'2014-01-01 00:00:00')
SET IDENTITY_INSERT [dbo].[Enquetes] OFF

SET IDENTITY_INSERT [dbo].[Questions] ON

INSERT INTO [dbo].[Questions] ([QuestionID], [QuestionNumber], [EnqueteID], [Text], [Type], [NextQuestionID], [NextQuestion_QuestionID]) VALUES (1, 1, 1, N'Wat is je naam?', 0, 0, 7)
INSERT INTO [dbo].[Questions] ([QuestionID], [QuestionNumber], [EnqueteID], [Text], [Type], [NextQuestionID], [NextQuestion_QuestionID]) VALUES (2, 2, 1, N'Geslacht?', 1, 0, 6)
INSERT INTO [dbo].[Questions] ([QuestionID], [QuestionNumber], [EnqueteID], [Text], [Type], [NextQuestionID], [NextQuestion_QuestionID]) VALUES (3, 3, 1, N'Leeftijd', 0, 0, 5)
INSERT INTO [dbo].[Questions] ([QuestionID], [QuestionNumber], [EnqueteID], [Text], [Type], [NextQuestionID], [NextQuestion_QuestionID]) VALUES (4, 4, 1, N'Platforms?', 2, 0, 4)
INSERT INTO [dbo].[Questions] ([QuestionID], [QuestionNumber], [EnqueteID], [Text], [Type], [NextQuestionID], [NextQuestion_QuestionID]) VALUES (5, 5, 1, N'Hoeveel sterren?', 3, 0, 3)
INSERT INTO [dbo].[Questions] ([QuestionID], [QuestionNumber], [EnqueteID], [Text], [Type], [NextQuestionID], [NextQuestion_QuestionID]) VALUES (6, 6, 1, N'Cijfer?', 4, 0, 2)
INSERT INTO [dbo].[Questions] ([QuestionID], [QuestionNumber], [EnqueteID], [Text], [Type], [NextQuestionID], [NextQuestion_QuestionID]) VALUES (7, 7, 1, N'Is dit een goeie enquete?', 5, 0, 1)
INSERT INTO [dbo].[Questions] ([QuestionID], [QuestionNumber], [EnqueteID], [Text], [Type], [NextQuestionID], [NextQuestion_QuestionID]) VALUES (8, 8, 1, N'Hoe goed past deze enquete bij jou?', 6, 0, NULL)
SET IDENTITY_INSERT [dbo].[Questions] OFF

SET IDENTITY_INSERT [dbo].[PossibleAnswers] ON
INSERT INTO [dbo].[PossibleAnswers] ([PossibleAnswerID], [QuestionID], [Text], [NextQuestionID], [Question_QuestionID], [NextQuestion_QuestionID], [Question_QuestionID1]) VALUES (1, 0, N'PC', 0, 5, NULL, 5)
INSERT INTO [dbo].[PossibleAnswers] ([PossibleAnswerID], [QuestionID], [Text], [NextQuestionID], [Question_QuestionID], [NextQuestion_QuestionID], [Question_QuestionID1]) VALUES (2, 0, N'Xbox', 0, 5, NULL, 5)
INSERT INTO [dbo].[PossibleAnswers] ([PossibleAnswerID], [QuestionID], [Text], [NextQuestionID], [Question_QuestionID], [NextQuestion_QuestionID], [Question_QuestionID1]) VALUES (3, 0, N'PS3', 0, 5, NULL, 5)
INSERT INTO [dbo].[PossibleAnswers] ([PossibleAnswerID], [QuestionID], [Text], [NextQuestionID], [Question_QuestionID], [NextQuestion_QuestionID], [Question_QuestionID1]) VALUES (4, 0, N'Man', 0, 7, NULL, 7)
INSERT INTO [dbo].[PossibleAnswers] ([PossibleAnswerID], [QuestionID], [Text], [NextQuestionID], [Question_QuestionID], [NextQuestion_QuestionID], [Question_QuestionID1]) VALUES (5, 0, N'Vrouw', 0, 7, NULL, 7)
SET IDENTITY_INSERT [dbo].[PossibleAnswers] OFF
