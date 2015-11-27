/// <binding BeforeBuild='build-less' ProjectOpened='watch' />
// For more information on how to configure a task runner, please visit:
// https://github.com/gulpjs/gulp

var gulp = require('gulp'),
    gutil = require('gulp-util');
//var debug = require('gulp-debug');
var less = require('gulp-less');
var minifyCSS = require('gulp-minify-css')
var jsx = require('gulp-jsx');

gulp.task('build-less', function () {
    return gulp.src('./EventPlanner.Bootstrap/Content/bootstrap/bootstrap.less')
        .pipe(less())
        .pipe(minifyCSS())
        //.pipe(debug({ title: 'less:', minimal: false }))
        .pipe(gulp.dest('./EventPlanner.Web/Content/'));
});

gulp.task('jsx-transform', function () {
    return gulp.src(['./EvenPlanner.Web/Scripts/Custom/*.jsx', './EvenPlanner.Web/Scripts/Custom/Vote/*.jsx'])
      .pipe(jsx({
          factory: 'React.createClass'
      }))
      .pipe(gulp.dest('./EvenPlanner.Web/Scripts/Compiled/'));
});

gulp.task('watch', function () {
    gulp.watch('./**/*.less', ['build-less']);
});
