var Spinner = React.createClass({
	propTypes: {
		imgUrl: React.PropTypes.string.isRequired
	},
	getDefaultProps: function() {
		return {
			show: true,
			marginVertical: 50,
			marginHorizontal: 50
		};
	},
	getDivStyle: function() { 
		return {
  			marginLeft: '50%',
  			marginTop: '0.5em',
		}
	}, 
	render: function() {
		return (
			<div style={this.getDivStyle()}>
				<img src={this.props.imgUrl} />
			</div>
		);
	}

});