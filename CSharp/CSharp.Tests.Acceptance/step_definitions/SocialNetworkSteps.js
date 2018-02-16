var {defineSupportCode} = require('cucumber');

defineSupportCode(function ({And, But, Given, Then, When}) {
    When(/^someone enters the command "([^"]*)"$/, function (arg1, callback) {
        callback.pending();
    });
});