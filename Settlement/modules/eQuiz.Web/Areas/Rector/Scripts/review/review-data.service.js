(function (angular) {

    angular
        .module("settlementModule")
        .factory("reviewDataService", reviewDataService);

    reviewDataService.$inject = ["$http"];

    function reviewDataService($http) {

        var service = {
            getStudents: getStudentsAjax,
        };

        return service;

        function getStudentsAjax() {
            var promise = $http.get('/StudentReview/GetStudentsList');          
            return promise;
        }
    }

})(angular);