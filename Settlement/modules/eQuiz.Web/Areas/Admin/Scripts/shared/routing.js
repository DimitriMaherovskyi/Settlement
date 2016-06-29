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
                .when('/Index/Settlement', {
                    templateUrl: '/Areas/Admin/Scripts/auto-settlement.html',
                    controller: 'AutoSettlementController',
                    controllerAs: 'asc',
                    resolve: {
                        studentsList: function (autoSettlementDataService) {
                            return autoSettlementDataService.getStudentsToSettle().then(function (respond) {
                                return respond.data;
                            })
                        }
                    }
                })
                .when('/Index/Accounts', {
                     templateUrl: '/Areas/Admin/Scripts/accounts.html',
                     controller: 'AccountsController',
                     controllerAs: 'ac',
                     resolve: {
                         accountsList: function (accountsDataService) {
                             return accountsDataService.getAccounts().then(function (respond) {
                                 return respond.data;
                             })
                         },
                         accountRoles: function (accountsDataService) {
                            return accountsDataService.getRoles().then(function (respond) {
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
                    templateUrl: '/Areas/Admin/Scripts/quote-review.html',
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
                .when('/Index/Hostel', {
                    templateUrl: '/Areas/Admin/Scripts/hostel-info.html',
                    controller: 'HostelInfoController',
                    controllerAs: 'hc',
                    resolve: {
                        hostelInfo: function (hostelInfoDataService, $location) {
                            var Id = $location.search().Id;
                            return hostelInfoDataService.getHostelInfo(Id);
                        },
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