(function (angular) {
    angular
        .module('settlementModule', ['ngRoute'])
        .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
            $routeProvider
                .when('/', {
                    templateUrl: '/Areas/Admin/Scripts/review.html',
                    controller: 'ReviewController',
                    controllerAs: 'rc',
                    resolve: {
                        studentsList: function (reviewDataService) {
                            return reviewDataService.getStudents().then(function (respond) {
                        return respond.data;
                    })
               }
               }
               }) 
                .when('/Index/Student', {
                    templateUrl: '/Areas/Admin/Scripts/student.html',
                    controller: 'StudentController',
                    controllerAs: 'sc',
                    resolve: {
                        studentInfo: function (studentDataService, $location) {
                            var Id = $location.search().Id;
                            return studentDataService.getStudentInfo(Id);
                            },

                        violations: function (studentDataService) {
                            return studentDataService.getViolations();//.then(function (respond) {
                            // return respond.data;
                            //})
                            }
                        },
                    reloadOnSearch: false
                })
                 .when('/Index/Rooms', {
                     templateUrl: '/Areas/Admin/Scripts/rooms-review.html',
                     controller: 'RoomsReviewController',
                     controllerAs: 'rrc',
                     resolve: {
                         hostels: function (roomsReviewDataService) {
                             return roomsReviewDataService.getHostels();//.then(function (respond) {
                             // return respond.data;
                             //})
                         },
                     },
                     reloadOnSearch: false
                 })

                .otherwise({ redirectTo: '/' });

            $locationProvider.html5Mode(true);
        }]);
}
)(angular);