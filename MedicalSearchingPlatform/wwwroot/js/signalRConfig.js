"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/signalRServer").build();

connection.on("LoadAllArticle", function () {
    location.href = '/ArticlesPage/Index';
});

connection.on("LoadAllSchedule", function () {
    location.href = '/WorkingSchedulePage/Index';
})

connection.on("LoadDoctorAppointments", function () {
    location.href = "/AppointmentPage/Index";
});

connection.on("LoadPatientAppointments", function () {
    location.href = "/AppointmentPage/History";
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});
