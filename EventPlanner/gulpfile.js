/// <binding BeforeBuild='build-less, build-vote-js, build-event-js' ProjectOpened='watch-css' />
// For more information on how to configure a task runner, please visit:
// https://github.com/gulpjs/gulp

var gulp = require('gulp');
var gutil = require('gulp-util');
//var debug = require('gulp-debug');
var less = require('gulp-less');
var minifyCSS = require('gulp-minify-css');


 /* ----------------- LESS files ----------------- */
gulp.task('build-less', function () {
    return gulp.src('./EventPlanner.Bootstrap/Content/bootstrap/bootstrap.less')
        .pipe(less())
        .pipe(minifyCSS())
        //.pipe(debug({ title: 'less:', minimal: false }))
        .pipe(gulp.dest('./EventPlanner.Web/Content/'));
});

gulp.task('watch-css', function () {
    gulp.watch('./**/*.less', ['build-less']);
});