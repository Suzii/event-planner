'use strict';

var Spinner = React.createClass({
	displayName: 'Spinner',

	propTypes: {
		imgUrl: React.PropTypes.string.isRequired
	},
	getDefaultProps: function getDefaultProps() {
		return {
			show: true,
			marginVertical: 50,
			marginHorizontal: 50
		};
	},
	getDivStyle: function getDivStyle() {
		return {
			marginLeft: '50%',
			marginTop: '0.5em'
		};
	},
	render: function render() {
		return React.createElement(
			'div',
			{ style: this.getDivStyle() },
			React.createElement('img', { src: this.props.imgUrl })
		);
	}

});