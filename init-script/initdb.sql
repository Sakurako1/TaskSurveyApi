-- Create Tables
CREATE TABLE "Surveys" (
    "Id" SERIAL PRIMARY KEY,
    "Info" TEXT NOT NULL
);

CREATE TABLE "Questions" (
    "Id" SERIAL PRIMARY KEY,
    "Title" VARCHAR(500) NOT NULL,
    "SurveyId" INT NOT NULL,
    CONSTRAINT "FK_Question_Survey" FOREIGN KEY ("SurveyId") REFERENCES "Surveys"("Id") ON DELETE CASCADE
);

CREATE INDEX "idx_Question_Survey" ON "Questions"("SurveyId");

CREATE TABLE "Answers" (
    "Id" SERIAL PRIMARY KEY,
    "Text" VARCHAR(255) NOT NULL,
    "QuestionId" INT NOT NULL,
    CONSTRAINT "FK_Answer_Question" FOREIGN KEY ("QuestionId") REFERENCES "Questions"("Id") ON DELETE CASCADE
);

CREATE INDEX "idx_Answer_Question" ON "Answers"("QuestionId");

CREATE TABLE "Interviews" (
    "Id" SERIAL PRIMARY KEY,
    "SurveyId" INT NOT NULL,
    "DateTime" TIMESTAMP NOT NULL,
    "Respondent" VARCHAR(255) NOT NULL,
    CONSTRAINT "FK_Interview_Survey" FOREIGN KEY ("SurveyId") REFERENCES "Surveys"("Id") ON DELETE CASCADE
);

CREATE INDEX "idx_interviews_surveyId" ON "Interviews" ("SurveyId");

CREATE TABLE "Results" (
    "Id" SERIAL PRIMARY KEY,
    "QuestionId" INT NOT NULL,
    "AnswerId" INT NOT NULL,
    "InterviewId" INT NOT NULL,
    "IsRight" BOOLEAN NULL,
    CONSTRAINT "FK_Result_Question" FOREIGN KEY ("QuestionId") REFERENCES "Questions"("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Result_Answer" FOREIGN KEY ("AnswerId") REFERENCES "Answers"("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Result_Interview" FOREIGN KEY ("InterviewId") REFERENCES "Interviews"("Id") ON DELETE CASCADE
);

-- Create Data
INSERT INTO "Surveys" ("Info")
VALUES 
    ('Customer Satisfaction Survey'),
    ('Employee Satisfaction Survey'),
    ('Product Feedback Survey');

-- Insert Questions for Survey 1
INSERT INTO "Questions" ("Title", "SurveyId")
VALUES 
    ('How satisfied are you with our service?', 1),
    ('Would you recommend us to a friend?', 1),
    ('How often do you use our service?', 1);

-- Insert Questions for Survey 2
INSERT INTO "Questions" ("Title", "SurveyId")
VALUES 
    ('How would you rate the work environment?', 2),
    ('Do you feel valued at work?', 2),
    ('Would you consider staying in this company for the next 5 years?', 2);

-- Insert Questions for Survey 3
INSERT INTO "Questions" ("Title", "SurveyId")
VALUES 
    ('How would you rate the quality of the product?', 3),
    ('How likely are you to purchase this product again?', 3),
    ('How satisfied are you with the price of the product?', 3);

-- Insert Answers for Question 1 (Survey 1)
INSERT INTO "Answers" ("Text", "QuestionId")
VALUES 
    ('Very Satisfied', 1),
    ('Satisfied', 1),
    ('Dissatisfied', 1);

-- Insert Answers for Question 2 (Survey 1)
INSERT INTO "Answers" ("Text", "QuestionId")
VALUES 
    ('Yes', 2),
    ('No', 2),
    ('Maybe', 2);

-- Insert Answers for Question 3 (Survey 1)
INSERT INTO "Answers" ("Text", "QuestionId")
VALUES 
    ('Daily', 3),
    ('Weekly', 3),
    ('Monthly', 3);

-- Insert Answers for Question 4 (Survey 2)
INSERT INTO "Answers" ("Text", "QuestionId")
VALUES 
    ('Excellent', 4),
    ('Good', 4),
    ('Fair', 4);

-- Insert Answers for Question 5 (Survey 2)
INSERT INTO "Answers" ("Text", "QuestionId")
VALUES 
    ('Yes', 5),
    ('No', 5),
    ('Sometimes', 5);

-- Insert Answers for Question 6 (Survey 2)
INSERT INTO "Answers" ("Text", "QuestionId")
VALUES 
    ('Yes', 6),
    ('No', 6),
    ('Maybe', 6);

-- Insert Answers for Question 7 (Survey 3)
INSERT INTO "Answers" ("Text", "QuestionId")
VALUES 
    ('Excellent', 7),
    ('Good', 7),
    ('Poor', 7);

-- Insert Answers for Question 8 (Survey 3)
INSERT INTO "Answers" ("Text", "QuestionId")
VALUES 
    ('Very Likely', 8),
    ('Likely', 8),
    ('Unlikely', 8);

-- Insert Answers for Question 9 (Survey 3)
INSERT INTO "Answers" ("Text", "QuestionId")
VALUES 
    ('Very Satisfied', 9),
    ('Satisfied', 9),
    ('Dissatisfied', 9);

-- Insert Interviews
INSERT INTO "Interviews" ("SurveyId", "DateTime", "Respondent")
VALUES
    (1, CURRENT_TIMESTAMP, 'John Doe'),
    (2, CURRENT_TIMESTAMP + INTERVAL '1 day', 'Jane Smith'),
    (3, CURRENT_TIMESTAMP + INTERVAL '2 days', 'Mike Johnson');
