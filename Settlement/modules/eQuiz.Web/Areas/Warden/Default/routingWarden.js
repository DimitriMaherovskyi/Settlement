(function (angular) {
    angular
        .module('settlementModule', ['ngRoute'])
        .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
            $routeProvider
                .when('/', {
                    templateUrl: '/Areas/Warden/Scripts/review.html',
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
                    templateUrl: '/Areas/Warden/Scripts/student.html',
                    controller: 'StudentController',
                    controllerAs: 'sc',
                    resolve: {
                        studentInfo: function (studentDataService, $location) {
                            var Id = $location.search().Id;
                            return studentDataService.getStudentInfo(Id).then(function (respond) {
                                return respond.data;
                            });
                        },

                        violations: function (studentDataService) {
                            return studentDataService.getViolations().then(function (respond) {
                                return respond.data;
                            });
                        },
                        hostels: function (studentDataService) {
                            return studentDataService.getHostels().then(function (respond) {
                                return respond.data;
                            });
                        },
                        rooms: function (studentDataService) {
                            return studentDataService.getRooms().then(function (respond) {
                                return respond.data;
                            });
                        }
                    },
                    reloadOnSearch: false
                })

                .when('/Index/Quotes', {
                    templateUrl: '/Areas/Warden/Scripts/quote-review.html',
                    controller: 'QuotesReviewController',
                    controllerAs: 'qrc',
                    resolve: {
                        quotesList: function (quoteReviewDataService) {
                            return quoteReviewDataService.getQuotes().then(function (respond) {
                             return respond.data;
                         })
                        },
                    },
                    reloadOnSearch: false
                })

                .when('/Index/Hostels', {
                    templateUrl: '/Areas/Warden/Scripts/hostel-review.html',
                    controller: 'HostelsReviewController',
                    controllerAs: 'hr',
                    resolve: {
                        hostelInfo: function (hostelsReviewDataService, $location) {
                            var Id = $location.search().Id;
                            return hostelInfoDataService.getHostelInfo(Id);
                        },
                    },
                    reloadOnSearch: false
                })
                 .when('/Index/Rooms', {
                     templateUrl: '/Areas/Warden/Scripts/hostel-review.html',
                     controller: 'RoomsReviewController',
                     controllerAs: 'rrc',
                     resolve: {
                         hostelsList: function (hostelsReviewDataService) {
                             return hostelReviewDataService.getHostels().then(function (respond) {
                                 return respond.data;
                             })
                         },
                     },
                     reloadOnSearch: false
                 })

                .otherwise({ redirectTo: '/' });

            $locationProvider.html5Mode(true);
        }]);
}
)(angular);