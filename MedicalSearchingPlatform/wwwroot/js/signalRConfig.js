"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/signalRServer").build();

connection.on("LoadAllArticle", function () {
	location.href = '/ArticlesPage/Index';
});

connection.start().catch(function (err) {
	return console.error(err.toString());
});
