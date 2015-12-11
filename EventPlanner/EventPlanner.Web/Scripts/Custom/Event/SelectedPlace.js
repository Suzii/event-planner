import React from 'react';

export class SelectedPlace extends React.Component {
  constructor(props) {
    super(props);
    this.render = this.render.bind(this);
  }

  render() {
    return (
      <div className="inline-block">
        <input type="hidden" value={this.props.place.Id} name={"Places["+this.props.index+"].Id"} /> 
        <input type="hidden" value={this.props.place.VenueId} name={"Places["+this.props.index+"].VenueId"} /> 
        <input type="hidden" value={this.props.place.EventId} name={"Places["+this.props.index+"].EventId"} /> 
        <input type="hidden" value={this.props.place.Name} name={"Places["+this.props.index+"].Name"} /> 
        <input type="hidden" value={this.props.place.AddressInfo} name={"Places["+this.props.index+"].AddressInfo"} /> 
        <input type="hidden" value={this.props.place.City} name={"Places["+this.props.index+"].City"} /> 
        <span className="label label-tag label-accent">{this.props.place.Name}
          <a><span className="glyphicon glyphicon-remove label-remove-button" onClick={(event) => {this.props.deleteCallback(event, this.props.place)}}></span>
            </a></span>  
      </div>
    );
  }
}

SelectedPlace.propTypes = {
    place: React.PropTypes.object.isRequired,
    index: React.PropTypes.number,
    deleteCallback: React.PropTypes.func
  };