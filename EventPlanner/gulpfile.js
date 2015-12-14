/// <binding BeforeBuild='build-less, webpack' ProjectOpened='watch-css' />
// For more information on how to configure a task runner, please visit:
// https://github.com/gulpjs/gulp

var gulp = require('gulp');
var gutil = require('gulp-util');
var less = require('gulp-less');
var minifyCSS = require('gulp-minify-css');
var webpack = require('webpack-stream');


 /* ----------------- LESS files ----------------- */
gulp.task('build-less', function () {
    return gulp.src('./EventPlanner.Bootstrap/Content/bootstrap/bootstrap.less')
        .pipe(less())
        .pipe(minifyCSS())
        .pipe(gulp.dest('./EventPlanner.Web/Content/'));
});

gulp.task('watch-css', function () {
    gulp.watch('./**/*.less', ['build-less']);
});


/* ----------------- Webpack task ----------------- */
gulp.task('webpack', function () {
    webpack(require('./webpack.config.js')).pipe(gulp.dest('./EventPlanner.Web/Scripts/Compiled/'));
});