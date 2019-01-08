var loginapp = angular.module('EditorApp', ['ngRoute'], function ($locationProvider) {
    $locationProvider.html5Mode(true); // see also the <base href="/" /> in <head>
});

loginapp.config(["$provide", function ($provide) {
    var rootUrl = $("#linkRoot").attr("href");
    $provide.constant('rootUrl', rootUrl);
}]);
    
loginapp.controller("EditorCtrl", function ($scope, $http, $window, $timeout, rootUrl, $location) {

    $scope.location = $location;   

    $http.get(rootUrl + 'document/GetDocumentByID?id=' + $scope.location.search()['id'])
        .then(function (response) { 

            $scope.Doc = response.data;

        });

    $scope.export = function () {

        $window.location.reload();

        $http.post('/document/exporttoword', {
            'fileName': $scope.Doc.FileName,
            'text': $scope.Doc.Text
        }).then(function (response) {
            
        });

    };

});