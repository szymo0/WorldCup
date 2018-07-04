using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageBroker2.Core.Sender.Logic.Exceptions;
using MessageBroker2.Core.Sender.Plugins;
using Serilog;

namespace MessageBroker2.Core.Sender.Logic
{
    public class ProcessMessageStrategies : IProcessMessageStrategies
    {
        private readonly ILogger _logger;
        private readonly IReadDataFor _reader;
        private readonly ISendDataFor _sender;

        public ProcessMessageStrategies(ILogger logger, IReadDataFor reader, ISendDataFor sender)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }

        public async Task<IProcessMessageRaport> Process()
        {
            _logger.Information("Start process message to send");
            List<IDataToSend> dataToSends;
            ProcessMessageRaport processMessageRaport = null;
            try
            {
                dataToSends = (await _reader.ReadData()).ToList();
                _logger.Verbose("Data readed {@readedMessageCount}",dataToSends.Count);
                if (dataToSends.Count == 0)
                {
                    _logger.Verbose("No data was return by the reader");
                    return new NotReadAnyDataToProcess();
                }

                processMessageRaport = new ProcessMessageRaport(dataToSends.Count);
            }
            catch (Exception ex)
            {
                _logger.Error(ex,"Error occured when read data");
                throw new ReaderDataSourceNotAvaliable(ex);
            }

            try
            {
                List<Task> tasks = new List<Task>();
                foreach (var data in dataToSends)
                {
                    _logger.Debug("Sending data @{data}",data);
                    tasks.Add(_sender.Send(data));
                }

                try
                {
                    Task.WaitAll(tasks.ToArray());
                }
                catch (AggregateException aex)
                {
                    _logger.Warning(aex,"There was an errors when sending messages");
                }

                foreach (var task in tasks)
                {
                    if (task.IsCompletedSuccessfully)
                    {
                        _logger.Debug("Add data as successful processed");
                        processMessageRaport.AddSuccesfulMessage();
                    }
                    else
                    {
                        _logger.Debug("Add data as unsuccessful processed");
                        processMessageRaport.AddUnsuccessfulMessage();
                    }
                }
            }
            catch (Exception ex) { _logger.Error(ex, "Error occured when sending messages"); }

            _logger.Information("Processed message raport: {@processMessageRaport}", processMessageRaport);
            return processMessageRaport;
        }
    }
}