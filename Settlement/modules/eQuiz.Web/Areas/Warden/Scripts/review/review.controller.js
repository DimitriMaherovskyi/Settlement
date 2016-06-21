﻿(function (angular) {
    angular.module('settlementModule').controller('ReviewController', ReviewController);

    ReviewController.$inject = ['$scope', '$filter', 'reviewDataService', 'studentsList'];

    function ReviewController($scope, $filter, reviewDataService, studentsList) {
        var vm = this;

        var orderBy = $filter('orderBy');
        vm.groupListOpened = false;
        vm.search = ''; // Represents search field on the form
        vm.myPredicate = null;
        vm.tablePage = 0; // Current table page
        vm.resultsPerPage = 10;
        vm.resultsCount = [10, 25, 50, 100]; // Possible numbers of results per page
        vm.selectedGroup = [];

        vm.headers = [
    {
        name: 'Name',
        field: 'name',
        predicateIndex: 0
    }, {
        name: 'Hostel',
        field: 'hostel',
        predicateIndex: 1
    }, {
        name: 'Room',
        field: 'room',
        predicateIndex: 2
    }, {
        name: 'Institute',
        field: 'institute',
        predicateIndex: 3
    }
        ];
        vm.students = [];

        function activate() {
            vm.students = studentsList;
            vm.students.forEach(function (currVal, index, array) {
                currVal.Id = currVal.Id.toString();
                currVal.Name = currVal.Name.toString();
             //   currVal.HostelNum = currVal.HostelNum.toString();
             //   currVal.Room = currVal.Room.toString();
                currVal.Institute = currVal.Institute.toString();
            }); // Converts received data to string values
            vm.groupList = GetUniquePropertyValues(vm.students, 'Institute'); // Property user group needs to be changed manualy    
            generatePredicate();
        };

        activate();

        function generatePredicate() {
            vm.myPredicate = [null, null, null, null];
        }; // Generates empty predicates that are used for ordering

        function clearPredicatesExcept(index) {
            var temp = vm.myPredicate[index];
            generatePredicate();
            vm.myPredicate[index] = temp;
        }; // Clears all predicates except the one with a specified index

        vm.refreshPredicate = function (index) {
            if (vm.myPredicate[index] === null) {
                var item = null;
                switch (index) {
                    case 0:
                        item = '+firstName';
                        break;
                    case 1:
                        item = '+surname';
                        break;
                    case 2:
                        item = '+hostel';
                        break;
                    case 3:
                        item = '+institute';
                        break;
                }
                vm.myPredicate[index] = item;
            }
            else if (vm.myPredicate[index][0] === '+') {
                vm.myPredicate[index] = '-' + vm.myPredicate[index].slice(1);
            }
            else if (vm.myPredicate[index][0] === '-') {
                vm.myPredicate[index] = null;
            }
            clearPredicatesExcept(index);
        }; // Changes the value of the predicate with specified index and clears all others 

        vm.direction = function (index) {
            if (vm.myPredicate) {
                if (vm.myPredicate[index] === null) {
                    return null;
                };
                if (vm.myPredicate[index][0] === '+') {
                    return true;
                };
                return false;
            };
            return null;
        }; // Gets the order direction of the predicate with specified index

        vm.order = function (predicate, reverse) {
            vm.students = orderBy(vm.students, predicate, reverse);
            vm.predicate = predicate;
        }; // Orders the data based on the specified predicate

        vm.numberOfPages = function () {
            return Math.ceil(vm.searchFiltered.length / vm.resultsPerPage);
        };

        vm.getNumber = function (num) {
            return new Array(num);
        };

        vm.goToPage = function (page) {
            vm.tablePage = page;
        };

        $scope.setSelectedGroup = function () { // DONT PUT THIS FUNCTION INTO VM! let it be in scope (because of 'this' in function)
            var id = this.group;

            if (vm.selectedGroup.toString().indexOf(id.toString()) > -1) {
                for (var i = 0; i < vm.selectedGroup.length; i++) {
                    if (vm.selectedGroup[i] === id) {
                        vm.selectedGroup.splice(i, 1);
                    }
                }
            } else {
                vm.selectedGroup.push(id);
            }
            return false;
        };

        vm.checkAll = function () {
            vm.selectedGroup = [];
            for (var i = 0; i < vm.groupList.length; i++) {
                vm.selectedGroup.push(vm.groupList[i]);
            }

        };

        vm.unCheckAll = function () {
            vm.selectedGroup = [];
        };

        function GetUniquePropertyValues(arrayToCheck, propertyName) {
            var flags = [];
            var output = [];
            for (var i = 0; i < arrayToCheck.length; i++) {
                if (flags[arrayToCheck[i][propertyName]]) {
                    continue;
                }

                flags[arrayToCheck[i][propertyName]] = true;
                output.push(arrayToCheck[i][propertyName]);
            }

            return output;
        }
                       
        vm.checkSymbol = "&#x2714";        
        vm.toggleDropDownElem = function (group) {            
            if (vm.selectedGroup.toString().indexOf(group.toString()) > -1) {
                return false;
            }
            return true;        
        }
    };

})(angular);