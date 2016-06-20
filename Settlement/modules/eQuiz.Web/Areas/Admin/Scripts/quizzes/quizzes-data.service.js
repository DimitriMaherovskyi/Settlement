(function (angular) {

    angular
        .module("settlementModule")
        .factory("quizzesDataService", quizzesDataService);

    quizzesDataService.$inject = ["$http"];

    function quizzesDataService($http) {

        var service = {            
            getQuizzes: getQuizzesAjax,
        };

        return service;

        function getQuizzesAjax(quizeId) {
            var promise = $http.get('/StudentReview/GetStudentsList');
            return promise;
        }
    }

})(angular);