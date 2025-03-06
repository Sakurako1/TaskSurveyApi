# TaskSurveyApi

В Заднии реализовно Апи с двумя методами:
1)Получение данных вопроса для отображения на фронте (текста вопроса и
вариантов ответа)
2)Сохранение результатов ответа на вопрос по кнопке “Далее”. Принимает
выбранные радиобаттоны, возвращает айди следующего вопроса.

SQL скрипты для создания таблиц и иницализации тестовыми данными находятся в папке init-script

Запуск приложения :
1)Скачайте архив с программой 
2)Раскпакуйте архив 
4)Перейдите в папку с распакованным проектом и выполните в терминале docker-compose up
После завершения сборки проекта, к нему можно будет подключиться через swagger по адресу : http://localhost:8080/swagger/index.html

P.S. После запуска приложения бд инициализиурется с тестовыми данными . 3 Анкеты - Survey , в каждой анкете по 3 вопроса - Question , в каждом вопросе по 3 варианта ответа - Answer , и 3 интервью - Interview.
По Url(u) - /api/surveys/{surveyId}/questions/{id} ,где {surveyId} - Id Анкеты , Id - Id Question - получаем список ответов.
По Url(u) - /api/interviews/{interviewId}/questions/{questionId}/answers , где interviewId - Id Interview , questionId - Id Question , при передаче значений происходится сохрнение в таблице Result , а на выходе получаем Id следующего Выпроса . После ответа на последний вопрос в рамках анкеты , получаем сообщение The questions have ended. 

![изображение](https://github.com/user-attachments/assets/2ff3e8f9-3181-4099-b701-94517ef16ce6)

![изображение](https://github.com/user-attachments/assets/b651b807-5880-4f28-bb2d-e42a95322210)

![изображение](https://github.com/user-attachments/assets/a169279d-d272-49f1-a886-3def8c1d8aca)

![изображение](https://github.com/user-attachments/assets/b02886ed-9bb3-41a1-bd86-bca3db3c4673)

