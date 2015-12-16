import React from 'react';
import Options from './Options';
import {ProgressBar} from './ProgressBar';

export class Graph extends React.Component {
	constructor(props) {
		super(props);
		this.getTotalNumberOfVotes = this.getTotalNumberOfVotes.bind(this);
		this.isGraphEmpty = this.isGraphEmpty.bind(this);
		this.getBarPercentageFor = this.getBarPercentageFor.bind(this);
		this.render = this.render.bind(this);
        this.calculatePercantage = this.calculatePercantage.bind(this);
        this.getRawPercentageFor = this.getRawPercentageFor.bind(this);
	}

    getTotalNumberOfVotes() {
        return this.props.totalNumberOfVoters || this.props.yesVoters.length + this.props.maybeVoters.length + this.props.noVoters.length;
    }

    isGraphEmpty() {
        return (this.props.yesVoters.length + this.props.maybeVoters.length + this.props.noVoters.length) === 0;
    }

    getRawPercentageFor(voteType){
        var x = 0;
        switch (voteType) {
          case Options.YES: x = this.props.yesVoters.length; break;
          case Options.MAYBE: x = this.props.maybeVoters.length; break;
          case Options.NO: x = this.props.noVoters.length; break;
        }

        return this.calculatePercantage(x, this.getTotalNumberOfVotes());
    }

    getBarPercentageFor(voteType){
        var  percentages = [];
        percentages.push(this.getRawPercentageFor(Options.YES));
        percentages.push(this.getRawPercentageFor(Options.MAYBE));
        percentages.push(this.getRawPercentageFor(Options.NO));
        
        var sum = percentages.reduce((a,b) => a + b);
        var max = -1;
        if(sum > 100) {
            max = Math.max(...percentages);
        }

        var raw = this.getRawPercentageFor(voteType);
        return (raw == max) ? raw-1 : raw;
    }

    calculatePercantage(option, total) {
        return Math.round((option*100) / total, 0);
    }

	render() {
		if (!this.isGraphEmpty())
        {
            return (
                <div className="progress">
                    <ProgressBar voteType={Options.YES} voters={this.props.yesVoters} percentage={this.getBarPercentageFor(Options.YES)} />
                    <ProgressBar voteType={Options.MAYBE} voters={this.props.maybeVoters} percentage={this.getBarPercentageFor(Options.MAYBE)} />
                    <ProgressBar voteType={Options.NO} voters={this.props.noVoters} percentage={this.getBarPercentageFor(Options.NO)} />
                </div>
            );
        }
        else
        {
            return(
                <div className="progress">
                    <div className="text-center text-success">
                        Be the first one to vote for this option! Total number of voters is {this.props.totalNumberOfVoters}.
                    </div>
                </div>
            );
        }
	}
}

Graph.propTypes = {
        yesVoters: React.PropTypes.array.isRequired,
        maybeVoters: React.PropTypes.array.isRequired,
        noVoters: React.PropTypes.array.isRequired,
        totalNumberOfVoters: React.PropTypes.number
    };