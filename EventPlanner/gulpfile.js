/// <binding BeforeBuild='build-less' ProjectOpened='watch' />
// For more information on how to configure a task runner, please visit:
// https://github.com/gulpjs/gulp

var gulp = require('gulp');
var gutil = require('gulp-util');
//var debug = require('gulp-debug');
var less = require('gulp-less');
var minifyCSS = require('gulp-minify-css')

// JSX and ES6
var sourcemaps = require("gulp-sourcemaps");
var babel = require("gulp-babel");
var concat = require("gulp-concat");

gulp.task('build-less', function () {
    return gulp.src('./EventPlanner.Bootstrap/Content/bootstrap/bootstrap.less')
        .pipe(less())
        .pipe(minifyCSS())
        //.pipe(debug({ title: 'less:', minimal: false }))
        .pipe(gulp.dest('./EventPlanner.Web/Content/'));
});

gulp.task('build-vote-js', function () {
    return gulp.src(['./EventPlanner.Web/Scripts/Custom/Vote/*.jsx'])
      .pipe(sourcemaps.init())
      .pipe(babel({
          presets: ['es2015', 'react']
      }))
      .pipe(concat("vote-bundle.js"))
      .pipe(sourcemaps.write("."))
      .pipe(gulp.dest('./EventPlanner.Web/Scripts/Compiled/'));
});

gulp.task('build-event-js', function () {
    return gulp.src(['./EventPlanner.Web/Scripts/Custom/Event/*.jsx', './EventPlanner.Web/Scripts/Custom/Event/*.js'])
      .pipe(sourcemaps.init())
      .pipe(babel({
          presets: ['es2015', 'react']
      }))
      .pipe(concat("event-bundle.js"))
      .pipe(sourcemaps.write("."))
      .pipe(gulp.dest('./EventPlanner.Web/Scripts/Compiled/'));
});

gulp.task('watch-css', function () {
    gulp.watch('./**/*.less', ['build-less']);
});

gulp.task('watch-vote-js', function () {
    gulp.watch(['./**/Scripts/Custom/Vote/*.jsx'], ['build-vote-js']);
});

gulp.task('watch-event-js', function () {
    gulp.watch(['./**/Scripts/Custom/Vote/*.jsx', './**/Scripts/Custom/Vote/*.js'], ['build-event-js']);
});