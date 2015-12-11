// Webpack
var webpack = require('webpack');

/* ----------------- WEBPACK Vote page JavaScript----------------- */
module.exports = {
    entry: [
      './EventPlanner.Web/Scripts/Custom/Vote/PageInit.js'
    ],
    output: {
        path: './EventPlanner.Web/Scripts/Compiled/',
        filename: "vote-bundle.js"
    },
    module: {
        loaders: [{
            test: /\.js?$/,
            exclude: /node_modules/,
            loader: 'babel-loader'
        }]
    },
    devtool: 'source-map'
};