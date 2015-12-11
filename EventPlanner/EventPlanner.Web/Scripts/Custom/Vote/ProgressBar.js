import React from 'react';
import Options from './Options';
import classNames from 'classnames';

export class ProgressBar extends React.Component {
	constructor(props) {
		super(props);
		this.getProgressBarClasses = this.getProgressBarClasses.bind(this);
		this.getInlineStyles = this.getInlineStyles.bind(this);
		this.getVotersAsString = this.getVotersAsString.bind(this);
		this.getTooltipTitle = this.getTooltipTitle.bind(this);
		this.render = this.render.bind(this);

	}

	getProgressBarClasses() {
      return {
          'progress-bar': true,
          'progress-bar-success': this.props.voteType === Options.YES,
          'progress-bar-gold': this.props.voteType === Options.MAYBE,
          'progress-bar-danger': this.props.voteType === Options.NO,
        }
    }

    getInlineStyles() {
        return {width: this.props.percentage + '%'};
    }

    getVotersAsString() {
        if(!this.props.voters || !this.props.voters.length) {
            return "";
        }

        return this.props.voters.reduce(((prev, curr) => prev + ', ' + curr));
    }

    getTooltipTitle() {
        return this.props.voteType + " voters!";
    }

	render() {
		// TODO add proper tooltion to display names of voters 
        return (
            <div className={classNames(this.getProgressBarClasses())} style={this.getInlineStyles()}
                data-toggle="popover" title={this.getTooltipTitle()} data-content={this.getVotersAsString()}>
                <span className="" >{this.props.voters.length}</span>
            </div>
        );
	}
}