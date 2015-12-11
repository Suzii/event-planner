import React from 'react';

export class Spinner extends React.Component {
	constructor(props) {
		super(props);
        this.getDivStyle = this.getDivStyle.bind(this);
        this.render = this.render.bind(this);
	}
	
	getDivStyle() { 
		return {
  			marginLeft: '50%',
  			marginTop: '0.5em',
		}
	}

	render() {
		return (
			<div style={this.getDivStyle()}>
				<img src={this.props.imgUrl} />
			</div>
		);
	}
}


Spinner.propTypes = {
		imgUrl: React.PropTypes.string.isRequired
	};

Spinner.defaultProps = {
		show: true,
		marginVertical: 50,
		marginHorizontal: 50
	};