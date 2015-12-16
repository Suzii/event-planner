import React from 'react';
import Options from './Options';
import {VoteOptionForm} from './VoteOptionForm';
import {Graph} from './Graph';

export class VoteOption extends React.Component {
    constructor(props) {
        super(props);
        this.getVotersFor = this.getVotersFor.bind(this);
        this.render = this.render.bind(this);
    }

    getVotersFor(voteType) {
        switch (voteType) {
          case Options.YES: return this.props.votes.yes;
          case Options.MAYBE: return this.props.votes.maybe;
          case Options.NO: return this.props.votes.no;
        }
    }
    
    render() {
        return (
            <div className="row">
                <div className="col-md-2 col-sm-3 col-xs-6">
                    <span className="text-primary bold">{this.props.title}</span> <br/>
                    <span className="text-muted">{this.props.desc}</span>
                </div>
            
                <div className="col-md-2 col-sm-3 col-xs-6 center-vertically">
                    <VoteOptionForm 
                        optionId={this.props.optionId} 
                        usersVoteId={this.props.usersVoteId}
                        onValueSelectedCallback={this.props.onVoteCallback} 
                        preSelectedValue={this.props.preSelectedValue} />
                </div>
                <div className="col-md-8 col-sm-6 col-xs-12 center-vertically" style={{marginTop: '0.8em'}}>
                    <Graph 
                        yesVoters={this.getVotersFor(Options.YES)} 
                        maybeVoters={this.getVotersFor(Options.MAYBE)} 
                        noVoters={this.getVotersFor(Options.NO)} 
                        totalNumberOfVoters={this.props.totalNumberOfVoters} />
                </div>
            </div>
        );
    }
}

VoteOption.propTypes = {
        optionId: React.PropTypes.string.isRequired,
        usersVoteId: React.PropTypes.string,
        title: React.PropTypes.string.isRequired,
        desc: React.PropTypes.string.isRequired,
        onVoteCallback: React.PropTypes.func.isRequired,
        preSelectedValue: React.PropTypes.string,
        votes: React.PropTypes.object.isRequired
    };