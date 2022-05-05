2nd

check DB preferences [visits]

backend - /
    npm i && dotnet build && dotnet run
frontend - /frontend-rowi-hw
    npm i && npm run dev
api url - /frontend-rowi-hw/api


DB
    patient - [id,name,gender,birthDate, address]
    doctor - [id, name]
    visit - [id,visitDate,visitPlace, symptoms, diagnosis, recommendations, patientId, doctorId, medicineId?]
    medicine - [id, name, useMethod, effects, sidEffects ]
------------------------------------
sqlcmd -S 127.0.0.1 -U SA
create database [db-name];
select name from sys.databases;
delete from [countries] where name = ['Russia']


dotnet ef migrations add Initial
dotnet ef database update

INSERT INTO patients (name, gender, birthDate, address) VALUES ('Peter L.', 'Male', ');
INSERT INTO patients (name, gender, birthDate, address) VALUES ('Peter L.', 'Male', (CAST('1998-12-25' AS DateTime)), 'Dublin, the Big st. 31');
INSERT INTO patients (name, gender, birthDate, address) VALUES ('Peter L.', 'Male', new DateTime(1898, 05, 03), 'St. Petersburg, Bolshaya st. 11a');


SET IDENTITY_INSERT visits ON


